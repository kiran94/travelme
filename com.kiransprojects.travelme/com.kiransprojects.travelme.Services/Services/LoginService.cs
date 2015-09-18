namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Service.Interfaces;
    using com.kiransprojects.travelme.Services.Interfaces;
    using com.kiransprojects.travelme.Services.Templates;
    using System;
    using System.Collections.Generic;
    using RazorEngine.Templating; 

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
        /// Logge Service
        /// </summary>
        private readonly ILoggerService _loggerService = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginService"/> class.
        /// </summary>
        /// <param name="repository">User Repository</param>
        /// <param name="passwordService">Password Service</param>
        /// <param name="mailService">Mail Service</param>
        /// <param name="loggerService">Logger Service</param>
        public LoginService(
            IUserEntityRepository repository, 
            IPasswordService passwordService,
            IMailService mailService, 
            ILoggerService loggerService)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("Login Repository");
            }

            if(passwordService == null)
            {
                throw new ArgumentNullException("Password Service");
            }

            if (mailService == null)
            {
                throw new ArgumentNullException("Mail Service");
            }

            if (loggerService == null)
            {
                throw new ArgumentNullException("Logger Service");
            }

            this._repository = repository;
            this._passwordService = passwordService;
            this._mailService = mailService;
            this._loggerService = loggerService; 
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

            RegistrationViewModel templateData = new RegistrationViewModel();
            templateData.ID = User.ID;
            templateData.FirstName = User.FirstName;
            templateData.Email = User.Email;
            string testBody = "Hello @FirstName, this still has to be configured"; 

            TemplateServiceSingleton.getInstance().RunCompile(testBody, "CacheID", typeof(RegistrationViewModel), templateData); 

            if(this._mailService.SendMessage(toList, "travelme", "Registration Confirmation", "You have been successfully registered", false))
            {
                this._loggerService.Log(new Log("Registration Email Sent!", false));
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

            if(this._repository.Authenticate(Email, Password, out Role))
            {
                return true; 
            }
            else
            {
                UserEntity user = this._repository.GetByEmail(Email);
                user.InvalidPasswordCount++;
                user.InvalidPasswordDate = DateTime.Now;
                this._repository.Update(user, false);
                return false; 
            }
        }

        /// <summary>
        /// Allows the user to reset thier password
        /// </summary>
        /// <param name="Email">Email of user to send too</param>
        /// <returns>Flag indicating if email was sent successfully</returns>
        public bool ForgotPassword(string Email)
        {
            if(string.IsNullOrEmpty(Email))
            {
                return false; 
            }

            UserEntity user = this._repository.GetByEmail(Email);

            if(user == null)
            {
                return false; 
            }

            user.PasswordReset = true;
            IList<string> to = new List<string>(); 
            to.Add(Email);

            string body = "";

            if(_mailService.SendMessage(to, "travelme", "Password Reset", body, false))
            {
                Log log = new Log(string.Format("Password Reset sent for {0} to {1}", user.ID, user.Email));
                this._loggerService.Log(log);
                return true; 
            }

            return false; 
        }
    }
}