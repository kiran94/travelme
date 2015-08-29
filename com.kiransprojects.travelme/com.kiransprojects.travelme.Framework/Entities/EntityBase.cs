namespace com.kiransprojects.travelme.Framework.Entities
{
    using System;

    /// <summary>
    /// Parent of all entities persisted in the database.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public virtual Guid ID { get; set; }

        /// <summary>
        /// Gets or sets a relational identifier
        /// </summary>
        public virtual Guid? RelationID { get; set; }
    }
}