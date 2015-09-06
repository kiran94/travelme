namespace com.kiransprojects.travelme.DataAccess.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using System;
    using System.Linq; 
    using System.Collections.Generic; 

    /// <summary>
    /// Trip Repository for interacting with the trip table
    /// Trip Specific CRUD goes here
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TripRepository : RepositoryBase<Trip>, ITripRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEntityRepository"/> class.
        /// </summary>
        public TripRepository(INhibernateHelper helper)
            : base(helper)
        {

        }

        /// <summary>
        /// Gets all the locations from all posts within a trip
        /// </summary>
        /// <param name="ID">ID of trip to query</param>
        /// <returns>Collection of locations</returns>
        public IList<Location> GetLocations(Guid ID)
        {
            Trip trip = this.GetByID(ID);
   
            if(trip != null)
            {
                IList<Location> Locations = new List<Location>();
                foreach (Post post in trip.Posts)
                {
                    Location Loc = new Location()
                    {
                        Date = post.PostDate,
                        Longitude = post.PostLong,
                        Latittude = post.PostLat
                    };

                    Locations.Add(Loc);
                }

                return Locations; 
            }
            return null; 
        }
    }
}