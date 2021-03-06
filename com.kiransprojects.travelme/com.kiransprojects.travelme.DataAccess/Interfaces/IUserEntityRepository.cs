﻿namespace com.kiransprojects.travelme.DataAccess.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;

    /// <summary>
    /// Contract for the UserEntity Repository
    /// </summary>
    public interface IUserEntityRepository : IRepository<UserEntity>
    {
        /// <summary>
        /// Authenticates if a user's credentials are correct
        /// </summary>
        /// <param name="Email">User's Email</param>
        /// <param name="password">User's Password</param>
        /// <param name="Role">User's Role to be set</param>
        /// <returns>Flag indicating if the user has been authenticated</returns>
        bool Authenticate(string Email, string password, out string Role, out Guid ID);

        /// <summary>
        /// Checks if the email already exists in the database
        /// </summary>
        /// <param name="Email">Email to check</param>
        /// <returns>Flag indicating if email exists</returns>
        bool isEmailInUse(string Email);

        /// <summary>
        /// Retrieves a user by an email
        /// </summary>
        /// <param name="Email">Email to filter by</param>
        /// <returns>User Entity</returns>
        UserEntity GetByEmail(string Email); 
    }
}