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
        public Guid ID { get; set; }
    }
}