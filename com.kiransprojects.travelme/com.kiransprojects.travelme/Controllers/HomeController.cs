namespace com.kiransprojects.travelme.Controllers
{
    using com.kiransprojects.travelme.Services.Interfaces;
    using System.Web.Mvc;

    /// <summary>
    /// Home Controller (Default)
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">Logger Service</param>
        public HomeController(
            ILoggerService logger) 
            : base(logger)
        {
 
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
    }
}