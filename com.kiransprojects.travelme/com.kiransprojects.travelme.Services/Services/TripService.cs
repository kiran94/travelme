namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Service.Interfaces;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Trip Service
    /// </summary>
    public class TripService : ITripService
    {
        /// <summary>
        /// Trip Repository Methods
        /// </summary>
        private readonly ITripRepository _tripRepository = null;

        /// <summary>
        /// Logger Service
        /// </summary>
        private readonly ILoggerService _loggerService = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="TripService"/> class.
        /// </summary>
        public TripService(
            ITripRepository TripRepository,
            ILoggerService LoggerService)
        {
            if (TripRepository == null)
            {
                this._loggerService.Log(new Log("NullReferenceException: Trip Repository in Trip Service", true));
                throw new NullReferenceException("Trip Repository, Null Trip Repository");
            }

            if (LoggerService == null)
            {
                this._loggerService.Log(new Log("NullReferenceException: Logger Service in Trip Service", true));
                throw new NullReferenceException("Logger Service, Null Logger Service");
            }

            this._tripRepository = TripRepository;
            this._loggerService = LoggerService;
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
            if (trip == null)
            {
                return false;
            }

            try
            {
                this._tripRepository.Insert(trip);
                return true;
            }
            catch (Exception e)
            {
                this._loggerService.Log(new Log(e.Message, true));
                return false;
            }
        }

        /// <summary>
        /// Passes an existing trip to update
        /// </summary>
        /// <param name="trip"></param>
        /// <returns>Updated trip, when does not exist returns null</returns>
        public Trip EditTrip(Trip trip)
        {
            if (trip == null)
            {
                return null;
            }

            try
            {
                if (this._tripRepository.GetByID(trip.ID) != null)
                {
                    this._tripRepository.Update(trip, true);
                    return trip;
                }
            }
            catch (Exception e)
            {
                this._loggerService.Log(new Log(e.Message, true));
            }

            return null;
        }

        /// <summary>
        /// Deletes a trip
        /// </summary>
        /// <param name="trip">trip to delete</param>
        /// <returns>flag indicating if trip was successfull</returns>
        public bool DeleteTrip(Trip trip)
        {
            if (trip == null || this._tripRepository.GetByID(trip.ID) == null)
            {
                return false;
            }

            try
            {
                this._tripRepository.Delete(trip);
                return true;
            }
            catch (Exception e)
            {
                this._loggerService.Log(new Log(e.Message, true));
                return false;
            }
        }

        /// <summary>
        /// Gets Locations for a specific trip
        /// </summary>
        /// <param name="ID">Trip ID</param>
        /// <returns>List of Locations</returns>
        public IList<Location> GetLocations(Guid ID)
        {
            Trip trip = this._tripRepository.GetByID(ID);
            if (trip != null)
            {
                IList<Location> Locations = new List<Location>();

                for (int i = 0; i < trip.Posts.Count; i++)
                {
                    Location CurrentLocation = new Location();
                    CurrentLocation.Latittude = trip.Posts[i].PostLat;
                    CurrentLocation.Longitude = trip.Posts[i].PostLong;
                    CurrentLocation.Date = trip.Posts[i].PostDate;

                    Locations.Add(CurrentLocation);
                }

                return Locations;

            }

            return null;
        }
    }
}