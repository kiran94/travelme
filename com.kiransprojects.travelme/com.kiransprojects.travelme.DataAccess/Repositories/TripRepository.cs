namespace com.kiransprojects.travelme.DataAccess.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;

    /// <summary>
    /// Trip Repository for interacting with the trip table
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TripRepository : RepositoryBase<Trip>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEntityRepository"/> class.
        /// </summary>
        public TripRepository(INhibernateHelper helper)
            : base(helper)
        {

        }

        //Trip specific CRUD goes here..
    }
}