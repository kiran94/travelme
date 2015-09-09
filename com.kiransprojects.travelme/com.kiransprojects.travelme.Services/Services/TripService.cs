﻿namespace com.kiransprojects.travelme.Services.Services
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

        /// <summary>
        /// Trip Repository Methods
        /// </summary>
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

        /// <summary>
        /// Passes a trip to the data access layer to persist
        /// </summary>
        /// <param name="trip">Trip to save</param>
        /// <returns>flag indicating if operation was successful</returns>
        public bool AddTrip(Trip trip)
        {
            if(trip == null)
            {
                return false; 
            }

            try
            {
                this._repository.Insert(trip);
                return true; 
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false; 
            }
        }

        /// <summary>
        /// Passes an existing trip to update
        /// </summary>
        /// <param name="trip"></param>
        /// <returns>updated trip</returns>
        public Trip EditTrip(Trip trip)
        {
            if (trip == null)
            {
                return null;
            }

            try
            {
                this._repository.Update(trip, true);
                return trip; 
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Deletes a trip
        /// </summary>
        /// <param name="trip">trip to delete</param>
        /// <returns>flag indicating if trip was successfull</returns>
        public bool DeleteTrip(Trip trip)
        {
            if (trip == null)
            {
                return false;
            }

            try
            {
                this._repository.Delete(trip);
                return true; 
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false; 
            }
        }

        public IList<Location> GetLocations(Guid ID)
        {
            throw new NotImplementedException();
        }
    }
}