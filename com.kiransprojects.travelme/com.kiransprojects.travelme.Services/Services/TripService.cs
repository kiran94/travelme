namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Service.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Trip Service
    /// </summary>
    public class TripService : ITripService
    {
        /// <summary>
        /// Trip Repository
        /// </summary>
        private readonly IRepository<Trip> _repository = null;

        private readonly ITripRepository _tripRepository = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="TripService"/> class.
        /// </summary>
        public TripService(IRepository<Trip> Repository, ITripRepository TripRepository)
        {
            if(Repository == null)
            {
                throw new NullReferenceException("Trip Repository, Null Repository"); 
            }
            if(TripRepository == null)
            {
                throw new NullReferenceException("Trip Repository, Null Trip Repository");
            }

            this._repository = Repository;
            this._tripRepository = TripRepository; 
        }

        /// <summary>
        /// Get all trips for a user
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <returns>List of all Trips</returns>
        public IList<Trip> GetTrips(Guid ID)
        {
            IList<Trip> list = new List<Trip>();  
            list = this._tripRepository.GetTrips(ID);
            return list; 
        }

        public bool AddTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public Framework.Entities.Trip EditTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public IList<Location> GetLocations(Guid ID)
        {
            throw new NotImplementedException();
        }
    }
}