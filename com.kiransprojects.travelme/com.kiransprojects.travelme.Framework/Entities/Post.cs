namespace com.kiransprojects.travelme.Framework.Entities
{
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// Represents a Post
    /// </summary>
    public class Post : EntityBase
    {
        /// <summary>
        /// Gets or sets the post description
        /// </summary>
        [Required]
        [Display(Name = "Post")]
        [StringLength(256, ErrorMessage = "{0} has a maximum of {1} characters")]
        public virtual string PostData { get; set; }

        /// <summary>
        /// Gets or sets the post latitude
        /// </summary>
        [Required]
        [Display(Name = "Latitude")]
        [StringLength(11, ErrorMessage = "{0} has a maximum of {1} characters")]
        public virtual string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the post latitude
        /// </summary>
        [Required]
        [Display(Name = "Longitude")]
        [StringLength(11, ErrorMessage = "{0} has a maximum of {1} characters")]
        public virtual string Longitude { get; set; }
    }
}