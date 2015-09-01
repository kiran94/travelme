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
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq; 

    /// <summary>
    /// Trip Repository Tests
    /// </summary>
    [TestFixture]
    public class TripRepositoryTests
    {
        /// <summary>
        /// Nhibernate Helper
        /// </summary>
        private NhibernateHelper helper = null;

        /// <summary>
        /// Mocking Of Configuration class
        /// </summary>
        private Mock<IDatabaseConfig> MockConfig = null;

        /// <summary>
        /// Trip Test Data
        /// </summary>
        private Trip TestTrip;

        /// <summary>
        /// Trip Test Data
        /// </summary>
        private Trip TestTrip2;

        /// <summary>
        /// Run before each test
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

            TestTrip = new Trip()
            {
                ID = Guid.Parse("6D8BCE5C-5681-472E-A1DC-97C5EA0C23FA"),
                TripName = "Thailand",
                TripDescription = "Thai!",
                TripLocation = "Backpacking",
                RelationID = Guid.Parse("9fc0b724-d55f-441d-a1ae-ec726d7737f7")
            };

           TestTrip2 = new Trip()
           {
               ID = Guid.Parse("AD495462-5CB7-4861-8A37-5A0836AA1344"),
               TripName = "TripTests",
               TripDescription = "UpdatingTests",
               TripLocation = "Tests",
               RelationID = Guid.Parse("4CB1882A-4285-4D05-972C-0E2A9B97FACB")
           };

        }
        /// <summary>
        /// Ensures trip can be retrieved 
        /// </summary>
        [Test]
        public void GetByID_ExistingEntity_Retrieved()
        {
            TripRepository Repository = new TripRepository(helper);
            Assert.AreEqual(TestTrip.ID, Repository.GetByID(TestTrip.ID).ID);
        }

        /// <summary>
        /// Ensures exception is raised when value is null
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetByID_NonExistingEntity_Retrieved()
        {
            TripRepository Repository = new TripRepository(helper);
            Assert.AreEqual(null, Repository.GetByID(Guid.NewGuid()).ID);
        }

        /// <summary>
        /// Ensures an existing entity can be updated 
        /// </summary>
        [Test]
        public void Update_ExistingEntity_SucessfullyUpdated()
        {
            Random rand = new Random();
            this.TestTrip2.TripLocation = string.Format("Updated Data: {0}", rand.NextDouble());

            TripRepository Repository = new TripRepository(helper);
            Repository.Update(this.TestTrip2, false);

            Assert.AreEqual(this.TestTrip2.TripLocation, Repository.GetByID(this.TestTrip2.ID).TripLocation); 
        }

        /// <summary>
        /// Ensures an non existing entity is saved
        /// </summary>
        [Test]
        public void Update_NonExistingEntity_Saved()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "Saved",
                RelationID = Guid.Parse("4CB1882A-4285-4D05-972C-0E2A9B97FACB")
            };

            TripRepository Repository = new TripRepository(helper);
            Repository.Update(trip, false);

            Assert.AreEqual(trip.ID, Repository.GetByID(trip.ID).ID); 
        }

        /// <summary>
        /// Ensures that trying to store a null entity will throw an exception
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Update_NullEntity_ExceptionThrown()
        {
            TripRepository Repository = new TripRepository(helper);
            Repository.Update(null, false);
        }

        /// <summary>
        /// Ensures an exception is thrown when invalid data is attempted to be stored
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void ExceptionThrownUpdate_InvalidEntity_ExceptionThrown()
        {
            TripRepository Repository = new TripRepository(helper);

            this.TestTrip2.TripName = "InvalidInvalidInvalidInvalid";
            Repository.Update(TestTrip2, false);
            Assert.AreEqual(this.TestTrip2.TripName, Repository.GetByID(this.TestTrip2.ID).TripName);
        }

        /// <summary>
        /// Ensures existing entities cannot be duplicated into database
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_ExistingEntity_ExceptionThrown()
        {
            TripRepository Repository = new TripRepository(helper);
            Repository.Insert(this.TestTrip); 
        }

        /// <summary>
        /// Ensures a new entity is stored into the database
        /// </summary>
        [Test]
        public void Insert_NonExistingEntity_Stored()
        {
            Trip toStore = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "InsertedFromTests",
                RelationID = Guid.Parse("4CB1882A-4285-4D05-972C-0E2A9B97FACB")
            };

            TripRepository Repository = new TripRepository(helper);
            Repository.Insert(toStore);

            Assert.AreEqual(toStore.ID, Repository.GetByID(toStore.ID).ID);

            Repository.Delete(toStore); 
        }

        /// <summary>
        /// Ensures a null entity cannot be stored
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullEntity_ExceptionThrown()
        {
            TripRepository Repository = new TripRepository(helper);
            Repository.Insert(null); 
        }

        /// <summary>
        /// Ensures a invalid entity cannot be persisted
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_InvalidEntity_ExceptionThrown()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "InvalidInvalidInvalidInvalid",
                RelationID = Guid.Parse("4CB1882A-4285-4D05-972C-0E2A9B97FACB")
            };

            TripRepository Repository = new TripRepository(helper);
            Repository.Insert(trip);

            Assert.AreEqual(null, Repository.GetByID(trip.ID).ID);
        }

        /// <summary>
        /// Ensures an existing entity can be deleted
        /// </summary>
        [Test]
        public void Delete_ExistingEntity_SuccessfullyDeleted()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "Deleted",
                RelationID = Guid.Parse("4CB1882A-4285-4D05-972C-0E2A9B97FACB")
            };

            TripRepository Repository = new TripRepository(helper);
            Repository.Insert(trip);
            Repository.Delete(trip);
            Trip deleted = Repository.GetByID(trip.ID); 

            Assert.AreEqual(null, deleted);
        }

        /// <summary>
        /// Ensures an non existing entity cannot be deleted
        /// </summary>
        [Test]
        public void Delete_NonExistingEntity_ExceptionThrown()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "Deleted",
                RelationID = Guid.Parse("4CB1882A-4285-4D05-972C-0E2A9B97FACB")
            };

            TripRepository Repository = new TripRepository(helper);
            Repository.Delete(trip); 
        }

        /// <summary>
        /// Ensures an exception is thrown when a null entity is passed
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullEntity_ExceptionThrown()
        {
            TripRepository Repository = new TripRepository(helper);
            Repository.Delete(null);
        }

        /// <summary>
        /// Ensures posts can be added and persisted to a trip
        /// </summary>
        [Test]
        public void InsertSubList_ExistingEntity()
        {
            Post newPost = new Post()
            {
                ID = Guid.NewGuid(),
                PostData = "AddedFromTest",
                PostDate = DateTime.Now,
                RelationID = this.TestTrip2.ID
            };

            this.TestTrip2.Posts.Add(newPost);
            TripRepository Repository = new TripRepository(helper);
            Repository.Update(this.TestTrip2, true);

            Assert.IsTrue(Repository.GetByID(this.TestTrip2.ID).Posts.Contains(newPost)); 
        }

       


    }
}