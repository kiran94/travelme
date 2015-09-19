namespace com.kiransprojects.travelme.Services.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;

    /// <summary>
    /// Contract for Login Services
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Registers the passed user
        /// </summary>
        /// <param name="User">User to registe</param>
        /// <returns>Registered UserEntity</returns>
        UserEntity RegisterUser(UserEntity User);

        /// <summary>
        /// Signs a User in
        /// </summary>
        /// <param name="Email">Email the user is registered under</param>
        /// <param name="Password">Password the user is registered with</param>
        /// <param name="Role">User's role to be set</param>
        /// <returns>Flag indicating if user is authenticated</returns>
        bool SignIn(string Email, string Password, out string Role, out Guid ID);

        /// <summary>
        /// Allows the user to reset thier password
        /// </summary>
        /// <param name="Email">Email account to reset</param>
        /// <returns>Flag indicates if email was sent correctly</returns>
        bool ForgotPassword(string Email);
    }
}