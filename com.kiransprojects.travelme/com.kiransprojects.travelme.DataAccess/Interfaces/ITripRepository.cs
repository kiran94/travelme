namespace com.kiransprojects.travelme.DataAccess.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contract for Trip Repository
    /// </summary>
    public interface ITripRepository
    {
        /// <summary>
        /// Gets all the locations from all posts within a trip
        /// </summary>
        /// <param name="ID">ID of trip to query</param>
        /// <returns>Collection of locations</returns>
        IList<Location> GetLocations(Guid ID);

        /// <summary>
        /// Gets all the trips for a user
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <returns>List of trips</returns>
        IList<Trip> GetTrips(Guid ID); 
    }
}