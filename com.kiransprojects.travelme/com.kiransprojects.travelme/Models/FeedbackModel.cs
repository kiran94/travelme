namespace com.kiransprojects.travelme.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Feedback Model used to provide feedback to views
    /// </summary>
    public class FeedbackModel
    {
        /// <summary>
        /// Gets or sets Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets whether the feedback is negative
        /// </summary>
        public bool isError { get; set; }
    }
}