namespace com.kiransprojects.travelme.Controllers
{
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Web.Mvc; 

    /// <summary>
    /// User Controller
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// User Service
        /// </summary>
        private readonly IUserService userService = null;

        /// <summary>
        /// Trip Service
        /// </summary>
        private readonly ITripService tripService = null;

        /// <summary>
        /// Post Service
        /// </summary>
        private readonly IPostService postService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">User Service</param>
        /// <param name="tripService">Trip Service</param>
        /// <param name="postService">Post Service</param>
        /// <param name="loggerService">Logger Service</param>
        public UserController(
            IUserService userService,
            ITripService tripService,
            IPostService postService,
            ILoggerService loggerService) 
            : base(loggerService)
        {
            if(this.userService == null)
            {
                throw new ArgumentNullException("User Service, User Controller");
            }

            if (this.tripService == null)
            {
                throw new ArgumentNullException("Trip Service, User Controller");
            }

            if (this.postService == null)
            {
                throw new ArgumentNullException("Post Service, User Controller");
            }

            this.userService = userService;
            this.tripService = tripService;
            this.postService = postService;
        }

        /// <summary>
        /// Home page for a logged in user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Home()
        {
            return this.View();
        }

    }
}
