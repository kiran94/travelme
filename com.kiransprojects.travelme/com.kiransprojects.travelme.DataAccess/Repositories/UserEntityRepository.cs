namespace com.kiransprojects.travelme.DataAccess.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate;

    /// <summary>
    /// User Entity Repository for interacting with the user table
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

        //User Entity specific CRUD goes here..
    }
}