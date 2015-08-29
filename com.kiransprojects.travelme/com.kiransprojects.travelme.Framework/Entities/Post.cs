namespace com.kiransprojects.travelme.Framework.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a Post
    /// </summary>
    public class Post : EntityBase
    {
          /// <summary>
        /// Initializes a new instance of the <see cref="Media"/> class.
        /// </summary>
        public Post()
        {
            this.Media = new List<Media>(); 
        }

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
        public virtual string PostLat { get; set; }

        /// <summary>
        /// Gets or sets the post latitude
        /// </summary>
        [Required]
        [Display(Name = "Longitude")]
        [StringLength(11, ErrorMessage = "{0} has a maximum of {1} characters")]
        public virtual string PostLong { get; set; }

        /// <summary>
        /// Gets or sets the media for the post
        /// </summary>
        [Display(Name="Media")]
        public virtual IList<Media> Media { get; set; }
    }
}
