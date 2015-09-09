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
            for(int i=0; i<3; i++)
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
        /// Ensures trips can be added
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
            Mock<IRepository<Trip>> Repository = new Mock<IRepository<Trip>>();
            Mock<ITripRepository> TripRepository = new Mock<ITripRepository>();
            Repository.Setup(o => o.Update(It.IsAny<Trip>(), true));

            TripService Service = new TripService(Repository.Object, TripRepository.Object);
            Trip trip = new Trip()
            {
                ID = Guid.NewGuid()
            };

            Trip Retrieved = Service.EditTrip(trip);
            Assert.AreEqual(trip, Retrieved); 
        }
    }
}