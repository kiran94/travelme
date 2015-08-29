namespace com.kiransprojects.travelme.DataAccess.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;

    /// <summary>
    /// Post Repository
    /// </summary>
    public class PostRepository : RepositoryBase<Post>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostRepository"/> class.
        /// </summary>
        public PostRepository(INhibernateHelper helper)
            : base(helper)
        {
           
        }

        //Post specific CRUD goes here
    }
}