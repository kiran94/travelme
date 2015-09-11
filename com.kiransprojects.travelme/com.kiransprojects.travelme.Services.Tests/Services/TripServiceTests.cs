namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Services;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Trip Service Tests
    /// </summary>
    [TestFixture]
    public class TripServiceTests
    {
        /// <summary>
        /// Ensures a list is returned when trips exist
        /// </summary>
        [Test]
        public void GetTrips_Existing_ListReturned()
        {
            IList<Trip> trips = new List<Trip>();
            for (int i = 0; i < 3; i++)
            {
                Trip trip = new Trip();
                trip.ID = new Guid();
                trip.TripName = i.ToString();
                trips.Add(trip);
            }

            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();
            TripRepository.Setup(o => o.GetTrips(It.IsAny<Guid>())).Returns(trips);

            TripService Service = new TripService(Repository.Object, TripRepository.Object);
            IList<Trip> list = Service.GetTrips(Guid.NewGuid());
            Assert.AreEqual(trips, list);
        }

        /// <summary>
        /// Ensures a null is returned when the trip does not exist
        /// </summary>
        [Test]
        public void GetTrips_NonExisting_Null()
        {
            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();
            TripRepository.SetupSequence(o => o.GetTrips(It.IsAny<Guid>())).Returns(null);

            TripService Service = new TripService(Repository.Object, TripRepository.Object);
            IList<Trip> list = Service.GetTrips(Guid.NewGuid());
            Assert.IsNull(list);
        }

        /// <summary>
        /// Ensrures trips can be added
        /// </summary>
        [Test]
        public void AddTrip_NonExistingEntity_Added()
        {
            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();
            Repository.Setup(o => o.Insert(It.IsAny<Trip>()));

            TripService Service = new TripService(Repository.Object, TripRepository.Object);
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid()
            };
            bool flag = Service.AddTrip(new Trip());
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Ensures trips are not added when already exist
        /// </summary>
        [Test]
        public void AddTrip_ExistingEntity_NotAdded()
        {
            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();
            Repository.Setup(o => o.Insert(It.IsAny<Trip>())).Throws<Exception>();

            TripService Service = new TripService(Repository.Object, TripRepository.Object);
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid()
            };
            bool flag = Service.AddTrip(new Trip());
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures trips can be updated
        /// </summary>
        [Test]
        public void EditTrip_ExistingEntity_Updated()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid()
            };

            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();
            Repository.Setup(o => o.Update(It.IsAny<Trip>(), true));
            Repository.SetupSequence(o => o.GetByID(It.IsAny<Guid>())).Returns(trip);

            TripService Service = new TripService(Repository.Object, TripRepository.Object);

            Trip Retrieved = Service.EditTrip(trip);
            Assert.AreEqual(trip, Retrieved);
        }

        /// <summary>
        /// Ensures non existing trips cannot be updated
        /// </summary>
        [Test]
        public void EditTrip_NonExistingEntity_NotEdited()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid()

            };

            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();
            Repository.Setup(o => o.Update(It.IsAny<Trip>(), true));
            Repository.SetupSequence(o => o.GetByID(It.IsAny<Guid>())).Returns(null);

            TripService Service = new TripService(Repository.Object, TripRepository.Object);
            Trip Result = Service.EditTrip(trip);

            Assert.AreEqual(null, Result);
        }

        /// <summary>
        /// Ensures a null entity passed will return false
        /// </summary>
        [Test]
        public void Delete_NullEntity_ReturnsFalse()
        {
            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();

            TripService Service = new TripService(Repository.Object, TripRepository.Object);
            bool Result = Service.DeleteTrip(null);
            Assert.IsFalse(Result);
        }

        /// <summary>
        /// Ensures true is returned when an existing entity is passed
        /// </summary>
        [Test]
        public void Delete_ExistingEntity_ReturnsTrue()
        {
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid()

            };

            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();

            Repository.Setup(o => o.Delete(It.IsAny<Trip>()));
            Repository.SetupSequence(o => o.GetByID(trip.ID)).Returns(trip);
            TripService Service = new TripService(Repository.Object, TripRepository.Object);

            Assert.IsTrue(Service.DeleteTrip(trip));
        }

        /// <summary>
        /// Ensures non existing entities return false when attempted to deleted
        /// </summary>
        [Test]
        public void Delete_NonExistingEntity_ReturnsFalse()
        {

            Trip trip = new Trip()
            {
                ID = Guid.NewGuid()

            };

            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();

            Repository.SetupSequence(o => o.GetByID(It.IsAny<Guid>())).Returns(null);

            TripService Service = new TripService(Repository.Object, TripRepository.Object);

            Assert.IsFalse(Service.DeleteTrip(trip));
        }

        /// <summary>
        /// Ensures when a trip exists with locations, list is returned
        /// </summary>
        [Test]
        public void GetLocations_TripExistsWithLocations_ReturnsList()
        {
            Guid tripID = Guid.NewGuid();
            Trip trip = new Trip()
            {
                ID = tripID,
                Posts = new List<Post>(),
            };

            Post Posts1 = new Post()
            {
                ID = Guid.NewGuid(),
                PostLat = "123",
                PostLong = "145",
                PostDate = DateTime.Now
            };

            Post Posts2 = new Post()
            {
                ID = Guid.NewGuid(),
                PostLat = "123",
                PostLong = "145",
                PostDate = DateTime.Now
            };

            trip.Posts.Add(Posts1);
            trip.Posts.Add(Posts2);

            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();

            Repository.Setup(o => o.GetByID(It.IsAny<Guid>())).Returns(trip); 

            TripService Service = new TripService(Repository.Object, TripRepository.Object);

            IList<Location> Locations = Service.GetLocations(Guid.NewGuid());

            CollectionAssert.IsNotEmpty(Locations);
        }

        /// <summary>
        /// Ensures null is returned when a trip does not exist
        /// </summary>
        [Test]
        public void GetLocations_NonExistingTrip_NullReturn()
        {
            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>(); 
            Repository.SetupSequence(o => o.GetByID(It.IsAny<Guid>())).Returns(null); 

            TripService Service = new TripService(Repository.Object, TripRepository.Object);
            IList<Location> Locations = Service.GetLocations(Guid.NewGuid());

            Assert.IsNull(Locations); 
        }

        /// <summary>
        /// Ensures an empty list is returned with when an existing trip has no locations
        /// </summary>
        [Test]
        public void GetLocations_TripWithNoLocations_EmptyList()
        {
            Guid tripID = Guid.NewGuid();
            Trip trip = new Trip()
            {
                ID = tripID,
                Posts = new List<Post>(),
            };

            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();

            Repository.Setup(o => o.GetByID(It.IsAny<Guid>())).Returns(trip);

            TripService Service = new TripService(Repository.Object, TripRepository.Object);

            IList<Location> Locations = Service.GetLocations(tripID);
            Assert.IsEmpty(Locations); 
        }
    }
}