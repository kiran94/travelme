namespace com.kiransprojects.travelme.DataAccess.Mappings
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.DataAccess.Repositories;
    using com.kiransprojects.travelme.Framework.Entities;

    /// <summary>
    /// Log Repository
    /// </summary>
    public class LogRepository : RepositoryBase<Log>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaMap"/> class.
        /// </summary>
        public LogRepository(INhibernateHelper helper)
            : base(helper)
        {

        }

        //Log specific CRUD here
    }
}