namespace com.kiransprojects.travelme.DataAccess.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate;
    using System.Collections.Generic;

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

        /// <summary>
        /// Authenticates a user credentials
        /// </summary>
        /// <param name="Username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User Role</returns>
        public string Authenticate(string Username, string password, out bool isAuthenticated)
        {
            using(ISession session = helper.GetSession())
            {
                using(ITransaction transaction = session.BeginTransaction())
                {
                    IList<UserEntity> users = session.QueryOver<UserEntity>()
                                                .Where(o => o.Email.Equals(Username))
                                                .Where(o => o.UserPassword.Equals(password))
                                                .List();

                    if(users == null && users[0] != null)
                    {
                        isAuthenticated = false; 
                        return string.Empty;  
                    }

                    isAuthenticated = true; 
                    return users[0].Role; 
                }
            }
        }
    }
}