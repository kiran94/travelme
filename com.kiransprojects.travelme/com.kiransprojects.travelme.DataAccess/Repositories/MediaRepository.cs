namespace com.kiransprojects.travelme.DataAccess.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;

    /// <summary>
    /// Media Repository
    /// </summary>
    public class MediaRepository : RepositoryBase<Media> 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostRepositoryMediaRepository"/> class.
        /// </summary>
        public MediaRepository(INhibernateHelper helper) 
            : base(helper)
        {
        }

        //Media specific CRUD here

    }
}