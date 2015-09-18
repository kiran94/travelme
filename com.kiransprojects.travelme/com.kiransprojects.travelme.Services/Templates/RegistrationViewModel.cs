namespace com.kiransprojects.travelme.Services.Templates
{
    using System;

    /// <summary>
    /// View Model used to generate Registration Email
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// Gets or sets ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the User URL 
        /// </summary>
        public string UserURL
        {
            get
            {
                string url = AppDomain.CurrentDomain.BaseDirectory;
                return string.Format("http://{0}/{1}", url, ID.ToString()); 
            }
        }
    }
}