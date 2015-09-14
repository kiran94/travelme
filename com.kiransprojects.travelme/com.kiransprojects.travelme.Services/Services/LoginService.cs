namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Service.Interfaces;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Collections.Generic;

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
        /// Password Service
        /// </summary>
        private readonly IPasswordService _passwordService = null;

        /// <summary>
        /// Mail Service
        /// </summary>
        private readonly IMailService _mailService = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginService"/> class.
        /// </summary>
        public LoginService(
            IUserEntityRepository repository, 
            IPasswordService passwordService,
            IMailService mailService)
        {
            if (repository == null)
            {
                throw new NotImplementedException("Login Repository");
            }

            if(passwordService == null)
            {
                throw new NotImplementedException("Password Service");
            }

            if (mailService == null)
            {
                throw new NotImplementedException("Password Service");
            }

            this._repository = repository;
            this._passwordService = passwordService;
            this._mailService = mailService; 
        }

        /// <summary>
        /// Verifies email is not already in use, generates password, saves user to database and sends confirmation email
        /// </summary>
        /// <param name="User">User to register</param>
        /// <returns>User if registered or null if not</returns>
        public UserEntity RegisterUser(UserEntity User)
        {
            if(User == null 
                ||string.IsNullOrEmpty(User.Email) 
                || this._repository.isEmailInUse(User.Email))
            {
                return null; 
            }

            User = this._passwordService.GenerateCredentials(User);

            if(User == null)
            {
                return null; 
            }

            this._repository.Insert(User);

            List<string> toList = new List<string>(); 
            toList.Add(User.Email);
            if(this._mailService.SendMessage(toList, "travelme", "Registration Confirmation", "You have been successfully registered", false))
            {
                return User; 
            }

            return null; 
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