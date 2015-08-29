namespace com.kiransprojects.travelme.Framework.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a Trip Entity
    /// </summary>
    public class Trip : EntityBase
    {
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
        /// Gets or sets posts for a current trip
        /// </summary>
        [Display(Name="Posts")]
        public virtual IList<Post> Posts { get; set; }
    }
}