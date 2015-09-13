namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Service.Interfaces;
    using System;

    /// <summary>
    /// Login Service
    /// </summary>
    public class LoginService : ILoginService
    {
        /// <summary>
        /// User Entity Repository
        /// </summary>
        private readonly IUserEntityRepository _repository = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginService"/> class.
        /// </summary>
        public LoginService(IUserEntityRepository repository)
        {
            if (repository == null)
            {
                throw new NotImplementedException("Login Repository");
            }

            this._repository = repository;
        }

        /// <summary>
        /// Registeres the user
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public UserEntity RegisterUser(UserEntity User)
        {
            //verify email not already in use
            this._repository.isEmailInUse(User.Email);

            //generate password

            //save to database
            return null;
            //send confirmation email
        }

        /// <summary>
        /// Authenticates if a user's credentials are correct
        /// </summary>
        /// <param name="Email">User's Email</param>
        /// <param name="password">User's Password</param>
        /// <param name="Role">User's Role to be set</param>
        /// <returns>Flag indicating if the user has been authenticated</returns>
        public bool SignIn(string Email, string Password, out string Role)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                Role =  string.Empty;
                return false;
            }

            return this._repository.Authenticate(Email, Password, out Role);
        }

        /// <summary>
        /// Allows the user to reset thier password
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public bool ForgotPassword(string Email)
        {
            throw new NotImplementedException();
        }
    }
}