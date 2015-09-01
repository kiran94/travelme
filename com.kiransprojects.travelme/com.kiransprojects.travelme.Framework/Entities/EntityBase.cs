namespace com.kiransprojects.travelme.Framework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Parent of all entities persisted in the database.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Required]
        public virtual Guid ID { get; set; }

        /// <summary>
        /// Gets or sets a relational identifier
        /// </summary>
        public virtual Guid? RelationID { get; set; }
    }
}