namespace com.kiransprojects.travelme.Framework.Entities
{
    using System; 
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification="Properties required to be virtual for Nhibernate")]
        public Post()
        {
            this.PostDate = null; 
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
        /// Gets or sets post date
        /// </summary>
        [Display(Name = "Date")]
        [DisplayFormat(NullDisplayText = "-")]
        public virtual DateTime? PostDate { get; set; }

        /// <summary>
        /// Gets or sets the media for the post
        /// </summary>
        [Display(Name="Media")]
        public virtual IList<Media> Media { get; set; }
    }
}