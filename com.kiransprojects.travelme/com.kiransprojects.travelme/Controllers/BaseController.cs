namespace com.kiransprojects.travelme.Controllers
{
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Base Controller inherited from all Controllers in the application
    /// </summary>
    public abstract class BaseController : Controller
    {        
        /// <summary>
        /// Logger Service
        /// </summary>
        private readonly ILoggerService loggerService = null; 

        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="loginService">Login Services</param>
        /// <param name="loggerService">Logger Service</param>
        public BaseController(
            ILoggerService loggerService)
        {
            if(loggerService == null)
            {
                throw new ArgumentNullException("Logger Service in Base Controller"); 
            }

            this.loggerService = loggerService; 
        }
    }
}