namespace com.kiransprojects.travelme.Controllers
{
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Framework.Enums;
    using com.kiransprojects.travelme.Models;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Security.Principal;
    using System.Web.Mvc;
    using System.Web.Security;

    /// <summary>
    /// Login Controller
    /// </summary>
    public class AccountController : BaseController
    {
        /// <summary>
        /// Login Service
        /// </summary>
        private readonly ILoginService _loginService = null;

        /// <summary>
        /// User Service
        /// </summary>
        private readonly IUserService _userService = null;

        /// <summary>
        /// Password Service
        /// </summary>
        private readonly IPasswordService _passwordService = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="logger">Logger Service</param>
        /// <param name="loginService">Login Service</param>
        /// <param name="userService">User Service</param>
        /// <param name="passwordService">Password Service</param>
        public AccountController(
            ILoggerService logger, 
            ILoginService loginService, 
            IUserService userService, 
            IPasswordService passwordService) 
            : base(logger)
        {
            if (loginService == null)
            {
                throw new ArgumentNullException("ILoginService, LoginController");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("IUserService, LoginController");
            }

            if(passwordService == null)
            {
                throw new ArgumentNullException("IPasswordService, LoginController");
            }

            this._loginService = loginService;
            this._userService = userService;
            this._passwordService = passwordService; 
        }

        /// <summary>
        /// Displays Login Screen, Used for authentication
        /// </summary>
        /// <returns>Returns Login Screen</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(UserViewModel userViewModel = null)
        {
            if(userViewModel == null)
            {
                return this.View(); 
            }

            return this.View(userViewModel.User); 
        }

        /// <summary>
        /// Posts Login data and checks if the model is valid
        /// </summary>
        /// <returns>Returns Login Screen on Fail and Home Screen on success</returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginRedirect(LoginModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Email/Password");
                return this.View("Login");
            }

            string Role = string.Empty;
            Guid ID = Guid.Empty;

            if (this._loginService.SignIn(
                userViewModel.Email,
                userViewModel.UserPassword,
                out Role,
                out ID))
            {
                UserEntity user = this._userService.GetUser(ID); 


                IIdentity identity = new GenericIdentity(user.Email);
                IPrincipal principle = new GenericPrincipal(identity, Role.Split(','));
                this.HttpContext.User = principle;

                FormsAuthentication.SetAuthCookie(user.ID.ToString(), false);
                return this.RedirectToAction("Home", "User", new { ID = ID});
            }

            //userViewModel.User.InvalidPasswordCount++;
            //userViewModel.User.InvalidPasswordDate = DateTime.Now;
            //this._userService.UpdateUser(userViewModel.User);

            //userViewModel.Feedback.Message = "Invalid Email/Password";
            //userViewModel.Feedback.isError = true;

            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Returns the Register View
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return this.View(); 
        }

        /// <summary>
        /// Validates the user model and posts to service
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(UserViewModel userViewModel)
        {
            if (userViewModel != null 
                && ModelState.IsValid)
            {
                userViewModel.User.ID = Guid.NewGuid(); 
                userViewModel.User.Registered = DateTime.Now;
                userViewModel.User.LastLogin = DateTime.Now;
                userViewModel.User.Role = RoleType.NormalUser;
                userViewModel.User.InvalidPasswordCount = 0;
                userViewModel.User.InvalidPasswordDate = null; 
                userViewModel.User = this._passwordService.GenerateCredentials(userViewModel.User); 

                bool flag = this._userService.AddUser(userViewModel.User);

                if(flag)
                {
                    return this.RedirectToAction("Home", "User");
                }
            }

            return this.View(userViewModel); 
        }

    }
}