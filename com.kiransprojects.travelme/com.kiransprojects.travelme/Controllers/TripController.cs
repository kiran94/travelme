namespace com.kiransprojects.travelme.Controllers
{
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Trip Controller
    /// </summary>
    [Authorize]
    public class TripController : BaseController
    {
        private readonly ITripService _tripService = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="TripController"/> class.
        /// </summary>
        /// <param name="logger">Logger Service</param>
        /// <param name="tripService">Trip Service</param>
        public TripController(
            ILoggerService logger, 
            ITripService tripService ) 
            : base(logger)
        {
            if(tripService == null)
            {
                throw new ArgumentNullException("Trip Service, Trip Controller"); 
            }

            this._tripService = tripService; 
        }
        
        /// <summary>
        /// Gets trips for the user
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Index(string ID)
        {
            if(string.IsNullOrEmpty(ID))
            {
                return this.RedirectToAction("Index", "Home"); 
            }

            IList<Trip> trips = this._tripService.GetTrips(Guid.Parse(ID)); 

            return this.View(trips);
        }
    }
}