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
    /// Media Repository Tests
    /// </summary>
    [TestFixture]
    public class MediaRepositoryTests
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
        /// Test Media
        /// </summary>
        private Media TestMedia;

        /// <summary>
        /// Test Media 2
        /// </summary>
        private Media TestMedia2;

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

            this.TestMedia = new Media()
            {
                ID = Guid.Parse("E66E84F1-F9AD-4F29-BA1C-6B38578EE2D8"),
                MediaData = "/Path/Content/Media.jpg",
                RelationID = Guid.Parse("522E9121-3F54-4127-B8A2-530B66E1C42C")
            };

            this.TestMedia2 = new Media()
          {
              ID = Guid.Parse("173EDD9C-DA41-4FA7-9DDA-07664E7DE271"),
              MediaData = "Path",
              RelationID = Guid.Parse("522E9121-3F54-4127-B8A2-530B66E1C42C")
          };
        }

        /// <summary>
        /// Ensures an existing entity can be retrieved
        /// </summary>
        [Test]
        public void GetByID_ExistingEntity_Retrieved()
        {
            MediaRepository Repository = new MediaRepository(helper);
            Assert.AreEqual(this.TestMedia.ID, Repository.GetByID(this.TestMedia.ID).ID);
        }

        /// <summary>
        /// Ensures an non existing entity returns null
        /// </summary>
        [Test]
        public void GetByID_NonExistingEntity_Null()
        {
            MediaRepository Repository = new MediaRepository(helper);
            Assert.AreEqual(null, Repository.GetByID(Guid.NewGuid()));
        }

        /// <summary>
        /// Ensures an non existing entity returns null
        /// </summary>
        [Test]
        public void Update_ExistingEntity_Updated()
        {
            MediaRepository Repository = new MediaRepository(helper);

            Random rand = new Random();
            this.TestMedia2.MediaData = rand.NextDouble().ToString();
            Repository.Update(this.TestMedia2, false);

            Assert.AreEqual(this.TestMedia2.MediaData, Repository.GetByID(this.TestMedia2.ID).MediaData);
        }

        /// <summary>
        /// Ensures non existing entities passed are saved
        /// </summary>
        [Test]
        public void Update_NonExistingEntity_Saved()
        {
            MediaRepository Repository = new MediaRepository(helper);

            Media media = new Media()
            {
                ID = Guid.NewGuid(),
                MediaData = "Update",
                RelationID = this.TestMedia2.RelationID
            };

            Repository.Update(media, false);

            Assert.AreEqual(media.ID, Repository.GetByID(media.ID).ID);
        }

        /// <summary>
        /// Ensures Exception is thrown when null entity is passed
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullEntity_ExceptionThrown()
        {
            MediaRepository Repository = new MediaRepository(helper);
            Repository.Update(null, false);
        }

        /// <summary>
        /// Ensures an exception is thrown when an invalid entity is attempted to be persisted
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Update_InvalidEntity_ExceptionThrown()
        {
            MediaRepository Repository = new MediaRepository(helper);
            this.TestMedia2.MediaData = "InvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidData";
            Repository.Update(TestMedia2, false);
        }

        /// <summary>
        /// Ensures an existing entity cannot be inserted as a new record
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_ExistingEntity_ExceptiontThrown()
        {
            MediaRepository Repository = new MediaRepository(helper);
            Repository.Insert(this.TestMedia2);
        }

        /// <summary>
        /// Ensures an non existing entity is inserted
        /// </summary>
        [Test]
        public void Insert_NonExistingEntity_Inserted()
        {
            MediaRepository Repository = new MediaRepository(helper);

            Media media = new Media()
            {
                ID = Guid.NewGuid(),
                MediaData = "Update",
                RelationID = this.TestMedia2.RelationID
            };

            Repository.Insert(media);
            Assert.AreEqual(media.ID, Repository.GetByID(media.ID).ID); 
        }

        /// <summary>
        /// Ensures a null entity cannot be be inserted
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullEntity_ExceptionThrown()
        {
            MediaRepository Repository = new MediaRepository(helper);
            Repository.Insert(null);
        }

        /// <summary>
        /// Ensures a invalid entity cannot be be inserted
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_InvalidEntity_ExceptionThrown()
        {
            MediaRepository Repository = new MediaRepository(helper);
            this.TestMedia2.MediaData = "InvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidDataInvalidData";
            Repository.Insert(TestMedia2);
        }

        /// <summary>
        /// Ensures record is deleted
        /// </summary>
        [Test]
        public void Delete_Existing_RecordDeleted()
        {
            MediaRepository Repository = new MediaRepository(helper);
            Media media = new Media()
            {
                ID = Guid.NewGuid(),
                MediaData = "Update",
                RelationID = this.TestMedia2.RelationID
            };

            Repository.Insert(media);
            Repository.Delete(media);
            Assert.IsNull(Repository.GetByID(media.ID));
        }

        /// <summary>
        /// Ensures record exception is thrown
        /// </summary>
        [Test]
        public void Delete_NonExisting_ExceptionThrown()
        {
            MediaRepository Repository = new MediaRepository(helper);
            Media media = new Media()
            {
                ID = Guid.NewGuid(),
                MediaData = "Update",
                RelationID = this.TestMedia2.RelationID
            };

            Repository.Delete(media);
            Assert.IsNull(Repository.GetByID(media.ID));
        }

        /// <summary>
        /// Ensures exception is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullEntity_ExceptionThrown()
        {
            MediaRepository Repository = new MediaRepository(helper);
            Repository.Delete(null);
        }
    }
}