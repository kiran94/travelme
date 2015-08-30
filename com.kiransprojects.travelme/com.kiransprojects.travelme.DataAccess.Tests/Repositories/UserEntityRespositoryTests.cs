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
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

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
        /// Test Existing User
        /// </summary>
        private UserEntity TestUser;

        /// <summary>
        /// Test Existing Trips
        /// </summary>
        private Trip TestTrip;

        /// <summary>
        /// Test Existing Trips 2
        /// </summary>
        private Trip TestTrip2;

        /// <summary>
        /// Test Existing Post
        /// </summary>
        private Post TestPost;

        /// <summary>
        /// Existng user to delete
        /// </summary>
        private UserEntity toDeleteUser;

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

            TestUser = new UserEntity()
            {
                ID = Guid.Parse("9fc0b724-d55f-441d-a1ae-ec726d7737f7"),
                FirstName = "Kiran",
                LastName = "Patel",
                DateOfBirth = new DateTime(1994, 08, 05, 10, 0, 0),
                Email = "Kiran@test.com",
                UserPassword = "password"
            };

            TestTrip = new Trip()
            {
                ID = Guid.Parse("209F9526-3611-4F30-A79C-55557FFBECF5"),
                TripName = "Australia",
                TripDescription = "Aussies",
                TripLocation = "Backpacking",
                RelationID = Guid.Parse("9fc0b724-d55f-441d-a1ae-ec726d7737f7")
            };

            TestTrip2 = new Trip()
            {
                ID = Guid.Parse("6D8BCE5C-5681-472E-A1DC-97C5EA0C23FA"),
                TripName = "Thailand",
                TripDescription = "Thai!",
                TripLocation = "Backpacking",
                RelationID = Guid.Parse("9fc0b724-d55f-441d-a1ae-ec726d7737f7")
            };

            TestPost = new Post()
            {
                ID = Guid.Parse("832B97D6-F958-497A-952D-0224F27C4E1A"),
                PostData = "PostName",
                PostLat = "Lat",
                PostLong = "Long",
                RelationID = Guid.Parse("209F9526-3611-4F30-A79C-55557FFBECF5")
            };

            TestTrip.Posts.Add(TestPost);
            TestUser.Trips.Add(TestTrip);
            TestUser.Trips.Add(TestTrip2);

            toDeleteUser = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Deleted",
                LastName = "Deleted",
                DateOfBirth = new DateTime(1994, 08, 05, 10, 0, 0),
                Email = "Deleted@Deleted.com",
                UserPassword = "Deleted"
            };

            UserEntityRepository ForDelete = new UserEntityRepository(helper);
            ForDelete.Insert(toDeleteUser);
            ForDelete = null;
        }

        /// <summary>
        /// Ensures existing entity in the database can be retrieved
        /// </summary>
        [Test]
        public void GetByID_ExistingEntity_RetrievedRecord()
        {
            UserEntityRepository RepositoryBase = new UserEntityRepository(helper);
            UserEntity Entity = RepositoryBase.GetByID(this.TestUser.ID);
            Assert.IsNotNull(Entity);
        }

        /// <summary>
        /// Ensures none existing entity in the database returns null
        /// </summary>
        [Test]
        public void GetByID_NonExistingEntity_ReturnsNull()
        {
            UserEntityRepository RepositoryBase = new UserEntityRepository(helper);
            UserEntity Entity = RepositoryBase.GetByID(Guid.NewGuid());
            Assert.IsNull(Entity);
        }

        /// <summary>
        /// Ensures a record can be updated
        /// </summary>
        [Test]
        public void Update_ExistingEntity_SuccessfullyUpdate()
        {
            Random rand = new Random();
            string UpdateData = string.Format("Password: {0}", rand.NextDouble().ToString());

            this.TestUser.UserPassword = UpdateData;

            UserEntityRepository RepositoryBase = new UserEntityRepository(helper);
            RepositoryBase.Update(this.TestUser, true);

            UserEntity Entity = RepositoryBase.GetByID(this.TestUser.ID);
            Assert.AreEqual(UpdateData, Entity.UserPassword);
        }

        /// <summary>
        /// Ensures non existing entities are saved
        /// </summary>
        [Test]
        public void Update_NonExistingEntity_Successfull()
        {
            UserEntity user = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "NonExisting",
                LastName = "NonExisting",
                DateOfBirth = DateTime.Now,
                Email = "NonExisting",
                UserPassword = "NonExisting"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Update(user, true);

            Assert.AreEqual(user.ID, Repository.GetByID(user.ID).ID);
        }

        /// <summary>
        /// Ensures null entities throw exception
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Update_NullEntity_NotSuccessfull()
        {
            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Update(null, true);
        }

        /// <summary>
        /// Ensures larger data then maximum cannot be stored
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Update_InvalidEntity_ExceptionThrown()
        {
            UserEntity user = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "NonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExisting",
                LastName = "NonExisting",
                DateOfBirth = DateTime.Now,
                Email = "NonExisting",
                UserPassword = "NonExisting"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Update(user, true);
        }

        /// <summary>
        /// Ensures an existing entity cannot be inserted (duiplicates)
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_ExistingEntities_ExceptionThrown()
        {
            UserEntity NewUser = this.TestUser;
            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Insert(NewUser);
        }

        /// <summary>
        /// Ensures new entities are inserted
        /// </summary>
        [Test]
        public void Insert_NonExistingEntities_Successfull()
        {
            UserEntity user = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "NonExisting",
                LastName = "NonExisting",
                DateOfBirth = DateTime.Now,
                Email = "NonExisting",
                UserPassword = "NonExisting"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Insert(user);

            UserEntity Retrieved = Repository.GetByID(user.ID);
            Assert.AreEqual(user.ID, Retrieved.ID);
        }

        /// <summary>
        /// Ensures null entities cannot be inserted
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Insert_NullEntity_ExceptionThrown()
        {
            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Insert(null);
        }

        /// <summary>
        /// Ensures invalid object are not inserted into the database
        /// </summary>
        [Test]
        [ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
        public void Insert_InvalidEntity_ExceptionThrown()
        {
            UserEntity user = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "NonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExistingNonExisting",
                LastName = "NonExisting",
                DateOfBirth = DateTime.Now,
                Email = "NonExisting",
                UserPassword = "NonExisting"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Insert(user);
        }

        /// <summary>
        /// Ensures existing entites can be deleted
        /// </summary>
        [Test]
        public void Delete_ExistingEntity_Successfull()
        {
            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Delete(toDeleteUser);

            UserEntity Retrieved = Repository.GetByID(toDeleteUser.ID);
            Assert.IsNull(Retrieved);
        }

        /// <summary>
        /// Ensures nothing is deleted when the entity does not exist
        /// </summary>
        [Test]
        public void Delete_NonExistingEntity_NothingDeleted()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "NonExisting",
                LastName = "NonExisting",
                Email = "NonExisting",
                UserPassword = "NonExisting"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Delete(Entity);
        }

        /// <summary>
        /// Ensures null entities throw exception
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullEntity_ExceptionThrown()
        {
            UserEntityRepository Repository = new UserEntityRepository(helper);
            Repository.Delete(null);
        }

        /// <summary>
        /// Ensures a trip is stored when added to an existing user
        /// </summary>
        [Test]
        public void InsertSubList_ExistingEntity_Successfull()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.Parse("B42B1A1E-9DD5-4904-B7CE-9D55FD9A547A"),
                FirstName = "SecondEntity",
                LastName = "SecondEntity",
                DateOfBirth = new DateTime(1994, 08, 05, 10, 0, 0),
                Email = "SecondEntity",
                UserPassword = "SecondEntity"
            };

            Trip trip = new Trip()
            {
                ID = Guid.Parse("053340CE-EE99-48B2-9687-693879933AFE"),
                TripName = "Trip",
                TripDescription = "Desc",
                TripLocation = "Location",
                RelationID = this.TestUser.ID
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            Entity.Trips.Add(trip);
            Repository.Update(Entity, true);

            IList<Trip> trips = Repository.GetByID(this.TestUser.ID).Trips
                .Where(o => o.ID == trip.ID)
                .ToList();

            Assert.That(Entity.Trips.Any(o => o.ID == trip.ID));
            TripRepository RepositoryTrip = new TripRepository(helper);
            RepositoryTrip.Delete(trip);
        }



    


    }
}