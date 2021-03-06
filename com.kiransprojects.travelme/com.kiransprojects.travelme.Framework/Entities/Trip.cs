namespace com.kiransprojects.travelme.Framework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a Trip Entity
    /// </summary>
    public class Trip : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trip"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Properties required to be virtual for Nhibernate")]
        public Trip()
        {
            this.Posts = new List<Post>();
        }

        /// <summary>
        /// Gets or sets the trip name
        /// </summary>
        [Required]
        [Display(Name = "Trip Name")]
        [StringLength(20, ErrorMessage = "{0} has a maximum of {1} characters")]
        public virtual string TripName { get; set; }

        /// <summary>
        /// Gets or sets the trip description
        /// </summary>
        [Required]
        [Display(Name = "Trip Description")]
        [StringLength(50, ErrorMessage = "{0} has a maximum of {1} characters")]
        public virtual string TripDescription { get; set; }

        /// <summary>
        /// Gets or sets the trip location
        /// </summary>
        [Required]
        [Display(Name = "Trip Location")]
        [StringLength(75, ErrorMessage = "{0} has a maximum of {1} characters")]
        public virtual string TripLocation { get; set; }

        /// <summary>
        /// Gets or sets Trip Created date time
        /// </summary>
        [Display(Name="Date Created")]
        public virtual DateTime? TripCreated { get; set; }

        /// <summary>
        /// Gets or sets posts for a current trip
        /// </summary>
        [Display(Name = "Posts")]
        public virtual IList<Post> Posts { get; set; }
    }
}