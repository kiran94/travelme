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
    /// User Entity Respository Tests
    /// </summary>
    [TestFixture]
    public class UserEntityRespositoryTests
    {
        /// <summary>
        /// Mocking for database config
        /// </summary>
        private Mock<IDatabaseConfig> MockConfig = null;

        /// <summary>
        ///Dependacy for Repository used to retrieve sessions
        /// </summary>
        private NhibernateHelper helper = null;

        /// <summary>
        /// SetUp function run before each test
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

            MockConfig = new Mock<IDatabaseConfig>();
            MockConfig.SetupSequence(o => o.GetConfig()).Returns(config);

            helper = new NhibernateHelper(MockConfig.Object);
        }

        /// <summary>
        /// Ensures that an existing entity in the database can be retrieved
        /// </summary>
        [Test]
        public void GetByID_ExistingEntity_RetrieveEntity()
        {
            UserEntity entity = new UserEntity()
            {
                ID = Guid.Parse("9fc0b724-d55f-441d-a1ae-ec726d7737f7"),
                FirstName = "TestFname",
                LastName = "TestLName",
                DateOfBirth = new DateTime(1994, 08, 05, 10, 00, 00),
                Email = "test@test.com",
                UserPassword = "password"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            UserEntity Returned = Repository.GetByID(entity.ID);

            Assert.AreEqual(entity.ID, Returned.ID);
            Assert.AreEqual(entity.FirstName, Returned.FirstName);
            Assert.AreEqual(entity.LastName, Returned.LastName);
            Assert.AreEqual(entity.DateOfBirth, Returned.DateOfBirth);
            Assert.AreEqual(entity.Email, Returned.Email);
            Assert.AreEqual(entity.UserPassword, Returned.UserPassword);
        }

        /// <summary>
        /// Ensures null is returned when an non existing entity is passed
        /// </summary>
        [Test]
        public void GetByID_NonExistingEntity_Null()
        {
            UserEntity entity = new UserEntity()
            {
                ID = Guid.Parse("9fc0b724-d55f-441d-a1be-ec726d7737f7"),
                FirstName = "fdsf",
                LastName = "TestsdfdsfLName",
                DateOfBirth = new DateTime(1934, 08, 05, 10, 00, 00),
                Email = "test@test.com",
                UserPassword = "password"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            UserEntity Returned = Repository.GetByID(entity.ID);

            Assert.AreEqual(null, Returned);
        }

        /// <summary>
        /// Ensures that a record can be stored successfully
        /// </summary>
        [Test]
        public void Insert_ValidEntity_StoredSuccessfully()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Unit",
                LastName = "Test",
                DateOfBirth = new DateTime(2012, 04, 01),
                Email = "unit@test.com",
                UserPassword = "password"
            };

            Guid StoredRecord = Entity.ID;

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Insert(Entity);

            UserEntity RetrievedEntity = Repository.GetByID(StoredRecord);
            Assert.AreEqual(Entity.ID, RetrievedEntity.ID);
        }

        /// <summary>
        /// Ensures that a null record is handled
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullEntity_NullException()
        {
            UserEntity Entity = null;
            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Insert(Entity);
        }

        /// <summary>
        /// Ensures update functionality works
        /// </summary>
        [Test]
        public void Update_ExistingEntity_SucessfullyUpdated()
        {
            Random rand = new Random();
            int UpdatedData = rand.Next(1, 1000);

            UserEntity entity = new UserEntity()
            {
                ID = Guid.Parse("9fc0b724-d56f-441d-a1ae-ec726d7737f7"),
                FirstName = "TestFname",
                LastName = "TestLName",
                DateOfBirth = new DateTime(1994, 08, 05, 10, 00, 00),
                Email = "test@test.com",
                UserPassword = UpdatedData.ToString()
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Update(entity, false);

            UserEntity RetrievedEntity = Repository.GetByID(entity.ID);

            Assert.AreEqual(entity.UserPassword, RetrievedEntity.UserPassword);
        }

        /// <summary>
        /// Ensures a non existing entity will still be inserted in to the database
        /// </summary>
        [Test]
        public void Update_NonExisting_InsertInto()
        {
            UserEntity entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "TestFname",
                LastName = "TestLName",
                DateOfBirth = new DateTime(1994, 08, 05, 10, 00, 00),
                Email = "test@test.com",
                UserPassword = "password"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Update(entity, false);

            Assert.AreEqual(entity.ID, Repository.GetByID(entity.ID).ID);
        }

        /// <summary>
        /// Ensures the delete functionality works
        /// </summary>
        [Test]
        public void Delete_ExistingEntity_SuccessfullyDeleted()
        {
            UserEntity entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "TestFname",
                LastName = "TestLName",
                DateOfBirth = new DateTime(1994, 08, 05, 10, 00, 00),
                Email = "test@test.com",
                UserPassword = "password"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Insert(entity);
            Repository.Delete(entity);

            UserEntity RetrievedEntity = Repository.GetByID(entity.ID);
            Assert.AreEqual(null, RetrievedEntity);
        }

        /// <summary>
        /// Ensures the delete functionality works for non existing
        /// </summary>
        [Test]
        public void Delete_NonExistingEntity_ReturnsNullNull()
        {
            UserEntity entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "TestFname",
                LastName = "TestLName",
                DateOfBirth = new DateTime(1994, 08, 05, 10, 00, 00),
                Email = "test@test.com",
                UserPassword = "password"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Delete(entity);

            UserEntity RetrievedEntity = Repository.GetByID(entity.ID);
            Assert.AreEqual(null, RetrievedEntity);
        }

        /// <summary>
        /// Ensures a trip is related to a user and can be accessed
        /// </summary>
        [Test]
        public void GetByID_TestWithTripList_ReturnsList()
        {
            Guid ID = Guid.Parse("51832A09-E6C9-4F01-8635-3A33FB724780");
            UserEntityRepository Repository  = new UserEntityRepository(helper);

            UserEntity Entity = Repository.GetByID(ID);

            Assert.AreEqual(2, Entity.Trips.Count);
        }
        
        
        /// <summary>
        /// Ensures a trip and transistively a post is related to a user and can be retrieved
        /// </summary>
        [Test]
        public void GetByID_TestWithTripListAndPost_ReturnsList()
        {
            Guid ID = Guid.Parse("51832A09-E6C9-4F01-8635-3A33FB724780");
            UserEntityRepository Repository = new UserEntityRepository(helper);
            UserEntity Entity = Repository.GetByID(ID);

            Assert.AreEqual(2, Entity.Trips.Count);
            Assert.AreEqual(1, Entity.Trips[0].Posts.Count);

        }

        /// <summary>
        /// Ensures exception is thrown when a user does not have any trips
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetByID_NonExistingTrips_ThrowsError()
        {
          Guid ID = Guid.Parse("51832A09-E6C9-4F01-8635-3A33FB724781");
          UserEntityRepository Repository = new UserEntityRepository(helper);
          UserEntity Entity = Repository.GetByID(ID);

          Assert.AreEqual(1, Entity.Trips.Count);
        }

        /// <summary>
        /// Ensures that a record can be stored successfully
        /// </summary>
        [Test]
        public void Insert_ValidTripEntity_StoredSuccessfully()
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

            UserEntityRepository Repository = new UserEntityRepository(helper);
            UserEntity RetrievedEntity = Repository.GetByID(user.ID);

            Trip trip = new Trip()
            {
                ID = Guid.NewGuid(),
                TripName = "NonExisting",
                TripDescription = "NonExisting",
                TripLocation = "NonExisting"
            };

            user.Trips = new System.Collections.Generic.List<Trip>();
            user.Trips.Add(trip);

            Repository.Update(user, true);

            RetrievedEntity = Repository.GetByID(user.ID);
            Assert.AreEqual(trip.ID, RetrievedEntity.Trips[RetrievedEntity.Trips.Count - 1].ID);
        }
    }
}