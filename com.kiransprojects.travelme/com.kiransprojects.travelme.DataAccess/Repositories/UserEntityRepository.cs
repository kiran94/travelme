﻿namespace com.kiransprojects.travelme.DataAccess.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// User Entity Repository for interacting with the user table
    /// </summary>
    public class UserEntityRepository : RepositoryBase<UserEntity>, IUserEntityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEntityRepository"/> class.
        /// </summary>
        public UserEntityRepository(INhibernateHelper helper)
            : base(helper)
        {
        }

        /// <summary>
        /// Authenticates if a user's credentials are correct
        /// </summary>
        /// <param name="Email">User's Email</param>
        /// <param name="password">User's Password</param>
        /// <param name="Role">User's Role to be set</param>
        /// <returns>Flag indicating if the user has been authenticated</returns>
        public bool Authenticate(string Email, string password, out string Role, out Guid ID)
        {
            using(ISession session = helper.GetSession())
            {
                using(ITransaction transaction = session.BeginTransaction())
                {
                    IList<UserEntity> users = session.QueryOver<UserEntity>()
                                                .Where(o => o.Email == Email)
                                                .Where(o => o.UserPassword == password)
                                                .List();



                    if(users == null || users.Count == 0 || users[0] == null)
                    {
                        ID = Guid.Empty;
                        Role = string.Empty;
                        return false; 
                    }
     
                    ID = users[0].ID; 
                    Role =  users[0].Role.ToString();
                    return true; 
                }
            }
        }

        /// <summary>
        /// Checks if the email already exists in the database
        /// </summary>
        /// <param name="Email">Email to check</param>
        /// <returns>Flag indicating if email exists</returns>
        public bool isEmailInUse(string Email)
        {
            using(ISession session = this.helper.GetSession())
            {
                using(ITransaction transaction = session.BeginTransaction())
                {
                    IList<UserEntity> users = session.QueryOver<UserEntity>()
                                            .Where(p => p.Email.Equals(Email))
                                            .List(); 
                    if(users != null && users[0] != null)
                    {
                        return true; 
                    }

                    return false; 
                }
            }
        }

        /// <summary>
        /// Gets a user by an email
        /// </summary>
        /// <param name="Email">Email to search by</param>
        /// <returns>User Entity</returns>
        public UserEntity GetByEmail(string Email)
        {
            using (ISession session = this.helper.GetSession())
            {
                using(ITransaction transaction = session.BeginTransaction())
                {
                    IList<UserEntity> users = session.QueryOver<UserEntity>()
                        .Where(o => o.Email == Email) 
                        .List();

                    if(users != null && users.Count != 0 && users[0] != null)
                    {
                        return users[0];
                    }

                    return null; 
                }
            }
        }
    }
}