namespace com.kiransprojects.travelme.Models
{
    using System.Linq; 
    using com.kiransprojects.travelme.Framework.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// User View Model
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Gets or sets the User Entity
        /// </summary>
        public UserEntity User 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets the welcome message for the user
        /// </summary>
        public string WelcomeMessage
        {
            get
            {
                string message = "Welcome Back";
                if(!string.IsNullOrEmpty(this.User.FirstName))
                {
                    return string.Format("{0}, {1}", message, this.User.FirstName); 
                }

                return message; 
            }
        }

        /// <summary>
        /// Gets the trips for displaying
        /// Orders by descending date of creation and takes top 5
        /// </summary>
        public IList<Trip> TripsForDisplay
        {
            get
            {
                if(this.User.Trips != null && this.User.Trips.Count != 0)
                {
                    return this.User.Trips
                        .OrderByDescending(o => o.TripCreated)
                        .Take(5) 
                        .ToList(); 
                }

                return null; 
            }
        }




        /// <summary>
        /// Gets or sets the feedback
        /// </summary>
        public FeedbackModel Feedback
        {
            get;
            set;
        }
    }
}