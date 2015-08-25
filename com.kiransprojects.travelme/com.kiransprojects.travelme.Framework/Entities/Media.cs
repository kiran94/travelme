namespace com.kiransprojects.travelme.Framework.Entities
{
    /// <summary>
    /// Represents a Media Entity
    /// </summary>
    public class Media : EntityBase
    {
        /// <summary>
        /// Gets or sets the media
        /// </summary>
        public virtual byte[] MediaData { get; set; }
    }
}
