namespace com.kiransprojects.travelme.Controllers
{
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Base Controller inherited from all Controllers in the application
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// User Service
        /// </summary>
        private readonly IUserService userService = null;
        
        /// <summary>
        /// Login Service
        /// </summary>
        private readonly ILoginService loginService = null;

        /// <summary>
        /// Logger Service
        /// </summary>
        private readonly ILoggerService loggerService = null; 

        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="userService">User Services</param>
        /// <param name="loginService">Login Services</param>
        /// <param name="loggerService">Logger Service</param>
        public BaseController(
            IUserService userService,
            ILoginService loginService,
            ILoggerService loggerService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("User Service in Base Controller");
            }

            if (loginService == null)
            {
                throw new ArgumentNullException("Login Service in Base Controller");
            }

            if(loggerService == null)
            {
                throw new ArgumentNullException("Logger Service in Base Controller"); 
            }

            this.userService = userService;
            this.loginService = loginService;
            this.loggerService = loggerService; 
        }
    }
}