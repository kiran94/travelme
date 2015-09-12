namespace com.kiransprojects.travelme.Framework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a Log
    /// </summary>
    public class Log : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Properties virtual for Nhibernate")]
        public Log()
        {
            this.ID = Guid.NewGuid(); 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Properties virtual for Nhibernate")]
        public Log(string Message="Error", bool Error = false)
        {
            this.ID = Guid.NewGuid(); 
            this.LogMessage = Message; 
            this.Error = Error;
            this.LogDateTime = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the Log message
        /// </summary>
        [StringLength(255, ErrorMessage = "{0} has a maximum length of {1}")]
        public virtual string LogMessage { get; set; }

        /// <summary>
        /// Indicates whether a log is an error
        /// </summary>
        public virtual bool Error { get; set; }

        /// <summary>
        /// Gets or sets date/time the log occured.
        /// </summary>
        public virtual DateTime LogDateTime { get; set; }
    }
}