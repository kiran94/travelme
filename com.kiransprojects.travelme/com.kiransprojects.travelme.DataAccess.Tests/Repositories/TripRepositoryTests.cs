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
    using System.Collections.Generic;
    using System.Reflection;

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
        }

        /// <summary>
        /// Ensures that an existing entity in the database can be retrieved
        /// </summary>
        [Test]
        public void GetByID_ExistingEntity_RetrieveEntity()
        {
            UserEntity user = new UserEntity()
            {
                ID= Guid.Parse("51832A09-E6C9-4F01-8635-3A33FB724780"),
                FirstName = "Kiran",
                LastName = "Patel",
                DateOfBirth = new DateTime(1994,08,05),
                Email = "Kiran@test.com",
                UserPassword = "password",
            };

            Trip trip = new Trip()
            {
                ID = Guid.Parse("209F9526-3611-4F30-A79C-55557FFBECF5"),
                TripName = "TestTrip",
                TripDescription = "TestDesc",
                TripLocation = "London"
            };

            user.Trips = new System.Collections.Generic.List<Trip>(); 
            user.Trips.Add(trip);

            TripRepository Repository = new TripRepository(helper);
            Trip Returned = Repository.GetByID(user.Trips[0].ID);

            Assert.AreEqual(trip.ID, Returned.ID);
            Assert.AreEqual(trip.TripName, Returned.TripName);
            Assert.AreEqual(trip.TripDescription, Returned.TripDescription);
            Assert.AreEqual(trip.TripLocation, Returned.TripLocation);
        }

        /// <summary>
        /// Ensures null is returned when an non existing entity is passed
        /// </summary>
        [Test]
        public void GetByID_NonExistingEntity_Null()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "NonExisting",
                TripDescription = "NonExisting",
                TripLocation = "NonExisting"
            };

            TripRepository Repository = new TripRepository(helper);
            Trip Returned = Repository.GetByID(trip.ID);

            Assert.AreEqual(null, Returned);
        }

        /// <summary>
        /// Ensures that a record can be stored successfully
        /// </summary>
        [Test]
        public void Insert_ValidEntity_StoredSuccessfully()
        {
            UserEntity user = new UserEntity()
            {
                ID = Guid.Parse("51832A09-E6C9-4F01-8635-3A33FB724780"),
                FirstName = "Kiran",
                LastName = "Patel",
                DateOfBirth = new DateTime(1994, 08, 05),
                Email = "Kiran@test.com",
                UserPassword = "password",
            };

            Trip trip = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "NonExisting",
                TripDescription = "NonExisting",
                TripLocation = "NonExisting"
            };

            user.Trips = new System.Collections.Generic.List<Trip>();
            user.Trips.Add(trip); 

            Guid StoredRecord = trip.ID;

            TripRepository Repository = new TripRepository(helper);
            Repository.Insert(user.Trips[0]);

            Trip RetrievedEntity = Repository.GetByID(StoredRecord);
            Assert.AreEqual(trip.ID, RetrievedEntity.ID);
        }

        /// <summary>
        /// Ensures that a null record is handled
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullEntity_NullException()
        {
            Trip Entity = null;
            TripRepository Repository = new TripRepository(helper);
            Repository.Insert(Entity);
        }

        /// <summary>
        /// Ensures update functionality works TODO
        /// </summary>
        [Test]
        public void Update_ExistingEntity_SucessfullyUpdated()
        {
            Random rand = new Random();
            int UpdatedData = rand.Next(1, 1000);

            Trip trip = new Trip()
            {
                ID = Guid.Parse("209F9526-3611-4F30-A79C-55557FFBECF5"),
                TripName = "TestTrip",
                TripDescription = "TestDesc",
                TripLocation = string.Format("Random - {0}", UpdatedData) 
            };

            TripRepository Repository = new TripRepository(helper);
            Repository.Update(trip);

            Trip RetrievedEntity = Repository.GetByID(trip.ID);

            Assert.AreEqual(trip.TripLocation, RetrievedEntity.TripLocation);
        }

        /// <summary>
        /// Ensures a non existing entity will still be inserted in to the database TODO
        /// </summary>
        [Test]
        public void Update_NonExisting_InsertInto()
        {
            Trip trip = new Trip()
            {
                ID = Guid.Parse("D5FBC552-BECE-4D4B-8FBE-ED1F52A31361"),
                TripName = "NonExisting",
                TripDescription = "TestDesc",
                TripLocation = "NonExisting"
            };

            TripRepository Repository = new TripRepository(helper);
            Repository.Update(trip);

            Assert.AreEqual(trip.ID, Repository.GetByID(trip.ID).ID);
        }

        /// <summary>
        /// Ensures the delete functionality works
        /// </summary>
        [Test]
        public void Delete_ExistingEntity_SuccessfullyDeleted()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "Deleted",
                TripDescription = "Deleted",
                TripLocation = "Deleted"
            };


            TripRepository Repository = new TripRepository(helper);
            Repository.Insert(trip);
            Repository.Delete(trip);

            Trip RetrievedEntity = Repository.GetByID(trip.ID);
            Assert.AreEqual(null, RetrievedEntity);
        }

        /// <summary>
        /// Ensures the delete functionality works for non existing
        /// </summary>
        [Test]
        public void Delete_NonExistingEntity_ReturnsNullNull()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "Deleted",
                TripDescription = "Deleted",
                TripLocation = "Deleted"
            };

            TripRepository Repository = new TripRepository(helper);
            Repository.Delete(trip);

            Trip RetrievedEntity = Repository.GetByID(trip.ID);
            Assert.AreEqual(null, RetrievedEntity);
        }

        /// <summary>
        /// Ensures a post is related to a trip and can be accessed TODO
        /// </summary>
        [Test]
        public void GetByID_TestWithPostList_ReturnsList()
        {
            Guid ID = Guid.Parse("209F9526-3611-4F30-A79C-55557FFBECF5");
            TripRepository Repository = new TripRepository(helper);

            Trip Entity = Repository.GetByID(ID);

            Assert.AreEqual(1, Entity.Posts);
        }

    }
}