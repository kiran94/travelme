namespace com.kiransprojects.travelme.DataAccess.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;

    /// <summary>
    /// User Entity Repository
    /// </summary>
    public class UserEntityRepository : RepositoryBase<UserEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEntityRepository"/> class.
        /// </summary>
        public UserEntityRepository(INhibernateHelper helper)
            : base(helper)
        {
        }
    
        //User specific crud goes here



    }

}