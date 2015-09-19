namespace com.kiransprojects.travelme.Models
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

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
        /// Gets or sets the feedback
        /// </summary>
        public FeedbackModel Feedback
        {
            get;
            set;
        }
    }
}