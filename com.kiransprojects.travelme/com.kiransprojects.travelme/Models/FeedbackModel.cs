namespace com.kiransprojects.travelme.Models
{

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