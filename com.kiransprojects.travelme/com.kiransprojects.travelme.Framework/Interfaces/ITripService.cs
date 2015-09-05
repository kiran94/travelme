namespace com.kiransprojects.travelme.Framework.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Constract for Trip services
    /// </summary>
    public interface ITripService
    {
        /// <summary>
        /// Get all trips for a user
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <returns>List of all Trips</returns>
        IList<Trip> GetTrips(Guid ID); 

        /// <summary>
        /// Adds a Trip
        /// </summary>
        /// <param name="trip">Trip to save</param>
        /// <returns>Returns a boolean indicating whether trip was saved</returns>
        bool AddTrip(Trip trip);

        /// <summary>
        /// Edits a trip
        /// </summary>
        /// <param name="trip">Trip to modify</param>
        /// <returns>Modified Trip</returns>
        Trip EditTrip(Trip trip);

        /// <summary>
        /// Deletes a trip
        /// </summary>
        /// <param name="trip">Trip to delete</param>
        /// <returns>Flag indicating if trip was deleted</returns>
        bool DeleteTrip(Trip trip);

        /// <summary>
        /// Gets Locations for a specific trip
        /// </summary>
        /// <param name="ID">Trip ID</param>
        /// <returns>List of Locations</returns>
        IList<Location> GetLocations(Guid ID);
    }
}