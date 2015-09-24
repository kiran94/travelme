namespace com.kiransprojects.travelme.Controllers
{
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
        private readonly ILoginService loginService = null;

        /// <summary>
        /// User Service
        /// </summary>
        private readonly IUserService userService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="logger">Logger Service</param>
        /// <param name="loginService">Login Service</param>
        /// <param name="userService">User Service</param>
        public AccountController(
            ILoggerService logger, 
            ILoginService loginService, 
            IUserService userService) 
            : base(logger)
        {
            if(logger == null)
            {
                throw new ArgumentNullException("ILoggerService, LoginController"); 
            }

            if (loginService == null)
            {
                throw new ArgumentNullException("ILoginService, LoginController");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("IUserService, LoginController");
            }

            this.loginService = loginService;
            this.userService = userService; 
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
        /// Posts login data
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginRedirect(UserViewModel userViewModel)
        {
            if (userViewModel == null
               || userViewModel.User == null
               || string.IsNullOrEmpty(userViewModel.User.Email)
               || string.IsNullOrEmpty(userViewModel.User.UserPassword)
               || !ModelState.IsValid)
            {
                return this.View();
            }

            string Role = string.Empty;
            Guid ID = Guid.Empty; 

            if (this.loginService.SignIn(
                userViewModel.User.Email, 
                userViewModel.User.UserPassword, 
                out Role, 
                out ID))
            {
                IIdentity identity = new GenericIdentity(userViewModel.User.Email);
                IPrincipal principle = new GenericPrincipal(identity, Role.Split(','));
                this.HttpContext.User = principle;
                FormsAuthentication.SetAuthCookie(userViewModel.User.Email, true);

                this.RedirectToAction("Index", "Profile"); 
            }

            userViewModel.User.InvalidPasswordCount++;
            userViewModel.User.InvalidPasswordDate = DateTime.Now;
            this.userService.UpdateUser(userViewModel.User); 

            userViewModel.Feedback.Message = "Invalid Email/Password";
            userViewModel.Feedback.isError = true;

            return this.RedirectToAction("Index", "Home", userViewModel);
        }


        
        

    }
}
