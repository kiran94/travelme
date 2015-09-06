namespace com.kiransprojects.travelme.DataAccess.Interfaces
{
    using System;
    using com.kiransprojects.travelme.Framework.Entities;

    /// <summary>
    /// Repository interface to be used for RepositoryBase
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Inserts a record into the database of type T
        /// </summary>
        /// <param name="Entity">Entity to insert</param>
        void Insert(T Entity);

        /// <summary>
        /// Gets entity of type T with the Guid ID
        /// </summary>
        /// <param name="ID">Unique identifier for entity</param>
        /// <returns>Entity that has ID</returns>
        T GetByID(Guid ID);

        /// <summary>
        /// Updates Entity of type T
        /// </summary>
        /// <param name="Entity">Entity to update</param>
        /// <param name="load">
        /// Indicates whether to load entity first, used for multiple sessions
        /// In multiple sessions the context of an entity will be lost so nhibernate will try to add new records when they already exist
        /// therefore we load the item first when inserting child entities. 
        /// </param>
        void Update(T Entity, bool load); 

        /// <summary>
        /// Deletes Entity of type T
        /// </summary>
        /// <param name="Entity">Entity to delete</param>
        void Delete(T Entity); 
    }
}