namespace com.kiransprojects.travelme.Framework.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a Media Entity
    /// </summary>
    public class Media : EntityBase
    {
        /// <summary>
        /// Gets or sets the media
        /// </summary>
        [Display(Name="Media")]
        [StringLength(1000, ErrorMessage="{0} has a maximum of {1} characters")]
        public virtual string MediaData { get; set; }
    }
}