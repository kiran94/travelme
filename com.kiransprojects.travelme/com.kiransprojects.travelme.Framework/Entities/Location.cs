namespace com.kiransprojects.travelme.Framework.Entities
{
    using System;

    /// <summary>
    /// Location Entity
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets or sets Latitude
        /// </summary>
        public string Latittude
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Longitude
        /// </summary>
        public string Longitude
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Date 
        /// </summary>
        public DateTime? Date
        {
            get;
            set;
        }

    }
}