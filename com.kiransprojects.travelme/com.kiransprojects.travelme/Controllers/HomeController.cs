namespace com.kiransprojects.travelme.Controllers
{
    using com.kiransprojects.travelme.Models;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Security.Principal;
    using System.Web.Mvc;
    using System.Web.Security;

    /// <summary>
    /// Home Controller (Default)
    /// </summary>
    public class HomeController : BaseController
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
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">Logger Service</param>
        /// <param name="loginService">Login Service</param>
        /// <param name="userService"> User Service</param>
        public HomeController(
            ILoggerService logger, 
            ILoginService loginService, 
            IUserService userService) 
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


            this.loginService = loginService; 
        }

        /// <summary>
        /// Displays the home page
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Retrieves post data from user and attempts to log in 
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserViewModel userViewModel)
        {
            throw new NotImplementedException(); 
        }
    }
}