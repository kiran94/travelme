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
        public void UpdateExistingEntity()
        {
            MediaRepository Repository = new MediaRepository(helper);
            Assert.AreEqual(null, Repository.GetByID(Guid.NewGuid()));
        }

        //TODO

        /// <summary>
        /// Ensures non existing entities passed are saved
        /// </summary>
        [Test]
        public void Update_NonExistingEntity_Saved()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures Exception is thrown when null entity is passed
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Update_NullEntity_ExceptionThrown()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures an exception is thrown when an invalid entity is attempted to be persisted
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Update_InvalidEntity_ExceptionThrown()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures an existing entity cannot be inserted as a new record
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_ExistingEntity_ExceptiontThrown()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures an non existing entity is inserted
        /// </summary>
        [Test]
        public void Insert_NonExistingEntity_Inserted()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures a null entity cannot be be inserted
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullEntity_ExceptionThrown()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures a invalid entity cannot be be inserted
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_InvalidEntity_ExceptionThrown()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures record is deleted
        /// </summary>
        [Test]
        public void Delete_Existing_RecordDeleted()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures record exception is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Delete_NonExisting_ExceptionThrown()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures exception is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullEntity_ExceptionThrown()
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Ensures a media entity can be related to a post entity
        /// </summary>
        [Test]
        public void InsertSubList_NonExistingEntity_Sucessfull()
        {
            throw new NotImplementedException(); 
        }
    }
}