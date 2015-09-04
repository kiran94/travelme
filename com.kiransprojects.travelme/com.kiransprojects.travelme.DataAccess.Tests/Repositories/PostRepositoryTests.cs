namespace com.kiransprojects.travelme.DataAccess.Tests.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.DataAccess.Repositories;
    using com.kiransprojects.travelme.Framework.Entities;
    using Moq;
    using NHibernate.Cfg;
    using NHibernate.Cfg.MappingSchema;
    using NHibernate.Dialect;
    using NHibernate.Driver;
    using NHibernate.Mapping.ByCode;
    using NUnit.Framework;
    using System;
    using System.Reflection; 

    /// <summary>
    /// Post Repository Tests
    /// </summary>
    [TestFixture]
    public class PostRepositoryTests
    {
        /// <summary>
        /// Nhibernate Helper Object
        /// </summary>
        private NhibernateHelper helper; 

        /// <summary>
        /// Mocking of Config Class
        /// </summary>
        private Mock<IDatabaseConfig> MockConfig; 

        /// <summary>
        /// Test Post
        /// </summary>
        private Post TestPost;

        /// <summary>
        /// Test Post 2
        /// </summary>
        private Post TestPost2; 

        /// <summary>
        /// Setup called before each test
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            Configuration config = new Configuration();
            config.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
                db.ConnectionString = "Data Source=DESKTOP-0II3UCP\\MAINSERVER;Initial Catalog=travelme;Integrated Security=True";
            });

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            config.AddMapping(mapping);

            this.MockConfig = new Mock<IDatabaseConfig>();
            this.MockConfig.SetupSequence(o => o.GetConfig()).Returns(config);

            this.helper = new NhibernateHelper(this.MockConfig.Object);

            this.TestPost = new Post()
            {
                ID = Guid.Parse("B1CF1D84-77B1-423B-9BC1-EDE7DB7C6B0B"),
                PostData = "Test",
                PostDate = new DateTime(2012,05,08, 10, 0, 0),
                PostLat = "123",
                PostLong = "456",
                RelationID = Guid.Parse("E50E7C95-36D3-470C-905E-564321267F20")
            };

            this.TestPost2 = new Post()
            {
                ID = Guid.Parse("5801D5D5-D945-44A2-8CDA-44DB247BAE11"),
                PostData = "Test2",
                PostDate = new DateTime(2012, 05, 08, 10, 0, 0),
                PostLat = "123",
                PostLong = "456",
                RelationID = Guid.Parse("E50E7C95-36D3-470C-905E-564321267F20")
            };


        }

        /// <summary>
        /// Ensures a post can be retrieved by id
        /// </summary>
        [Test]
        public void GetByID_ExistingEntity_SuccessfullyRetrieved()
        {
            PostRepository Repository = new PostRepository(helper);
            Post post = Repository.GetByID(this.TestPost.ID);
            Assert.AreEqual(this.TestPost.ID, post.ID); 
        }

        /// <summary>
        /// Ensures a non existing post returns null
        /// </summary>
        [Test]
        public void GetByID_NonExistingEntity_NullReturned()
        {
            PostRepository Repository = new PostRepository(helper);
            Post post = Repository.GetByID(Guid.NewGuid());
            Assert.IsNull(post); 
        }

        /// <summary>
        /// Ensures an existing entity can be updated
        /// </summary>
        [Test]
        public void Update_ExistingEntity_SuccessfullyUpdated()
        {
            Random rand = new Random(); 
            this.TestPost2.PostLat = rand.NextDouble().ToString();
            this.TestPost2.PostLat = this.TestPost2.PostLat.Substring(0, 10);

            PostRepository Repository = new PostRepository(helper);
            Repository.Update(this.TestPost2, false);
            Assert.AreEqual(this.TestPost2.PostLat, Repository.GetByID(this.TestPost2.ID).PostLat); 
        }

        /// <summary>
        /// Ensures non existing entities passed are saved
        /// </summary>
        [Test]
        public void Update_NonExistingEntity_Saved()
        {
            Post post = new Post()
            {
                ID = Guid.NewGuid(),
                PostData = "Updated Saved",
                PostDate = DateTime.Now,
                RelationID = Guid.Parse("E50E7C95-36D3-470C-905E-564321267F20")
            }; 

            PostRepository Repository = new PostRepository(helper); 
            Repository.Update(post, false);

            Assert.AreEqual(post.ID, Repository.GetByID(post.ID).ID);
        }

        /// <summary>
        /// Ensures Exception is thrown when null entity is passed
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Update_NullEntity_ExceptionThrown()
        {
            PostRepository Repository = new PostRepository(helper);
            Repository.Update(null, false); 
        }

        /// <summary>
        /// Ensures an exception is thrown when an invalid entity is attempted to be persisted
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Update_InvalidEntity_ExceptionThrown()
        {
            PostRepository Repository = new PostRepository(helper);

            Post post = new Post()
            {
                ID = Guid.NewGuid(),
                PostData = "InvalidData",
                PostLat = "123456789123456",
                PostDate = DateTime.Now,
                RelationID = Guid.Parse("E50E7C95-36D3-470C-905E-564321267F20")
            };

            Repository.Update(post, false); 
        }

        /// <summary>
        /// Ensures an existing entity cannot be inserted as a new record
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_ExistingEntity_ExceptiontThrown()
        {
            PostRepository Repository = new PostRepository(helper); 
            Post Existing = Repository.GetByID(this.TestPost2.ID);
            Repository.Insert(Existing);
        }

        /// <summary>
        /// Ensures an non existing entity is inserted
        /// </summary>
        [Test]
        public void Insert_NonExistingEntity_Inserted()
        {
            Post post = new Post()
            {
                ID = Guid.NewGuid(),
                PostData = "Data",
                PostDate = DateTime.Now,
                RelationID = this.TestPost2.RelationID
            };

            PostRepository Repository = new PostRepository(helper);
            Repository.Insert(post);

            Assert.AreEqual(post.ID, Repository.GetByID(post.ID).ID);
        }

        /// <summary>
        /// Ensures a null entity cannot be be inserted
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullEntity_ExceptionThrown()
        {
            PostRepository Repository = new PostRepository(helper);
            Repository.Insert(null);
        }

        /// <summary>
        /// Ensures a invalid entity cannot be be inserted
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_InvalidEntity_ExceptionThrown()
        {
            PostRepository Repository = new PostRepository(helper);

            Post post = new Post()
            {
                ID = Guid.NewGuid(),
                PostData = "InvalidData",
                PostLat = "123456789123456",
                PostDate = DateTime.Now,
                RelationID = Guid.Parse("E50E7C95-36D3-470C-905E-564321267F20")
            };

            Repository.Insert(post);
        }

        /// <summary>
        /// Ensures record is deleted
        /// </summary>
        [Test]
        public void Delete_Existing_RecordDeleted()
        {
            Post post = new Post()
            {
                ID = Guid.NewGuid(),
                PostData = "Data",
                PostDate = DateTime.Now,
                RelationID = this.TestPost2.RelationID
            };

            PostRepository Repository = new PostRepository(helper);
            Repository.Insert(post);
            Repository.Delete(post);
            
            Assert.AreEqual(null, Repository.GetByID(post.ID)); 
        }

        /// <summary>
        /// Ensures record exception is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Delete_NonExisting_ExceptionThrown()
        {
            Post post = new Post()
            {
                ID = Guid.NewGuid(),
                PostData = "Data",
                PostDate = DateTime.Now,
                RelationID = this.TestPost2.RelationID
            };

            PostRepository Repository = new PostRepository(helper);
            Repository.Delete(post);

            Assert.IsNull(Repository.GetByID(post.ID).ID);
        }

        /// <summary>
        /// Ensures exception is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullEntity_ExceptionThrown()
        {
            PostRepository Repository = new PostRepository(helper);
            Repository.Delete(null);
        }

        /// <summary>
        /// Ensures a media entity can be related to a post entity
        /// </summary>
        [Test]
        public void InsertSubList_NonExistingEntity_Sucessfull()
        {
            PostRepository Repository = new PostRepository(helper); 
            Media media = new Media()
            {
                ID = Guid.NewGuid(),
                MediaData = "test/test/test",
                RelationID = this.TestPost2.ID
            };

            this.TestPost2.Media.Add(media);
            Repository.Update(this.TestPost2, true);


            new MediaRepository(helper).Delete(media); 
        }

        /// <summary>
        /// Ensures a media entity can be related to a post entity
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void InsertSubList_InvalidEntity_Sucessfull()
        {
            PostRepository Repository = new PostRepository(helper);
            Media media = new Media()
            {
                ID = Guid.NewGuid(),
                MediaData = "test/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/tes/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/testtest/test/test",
                RelationID = this.TestPost2.ID
            };

            this.TestPost2.Media.Add(media);
            Repository.Update(this.TestPost2, true);
        }
    }
}