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

    }
}