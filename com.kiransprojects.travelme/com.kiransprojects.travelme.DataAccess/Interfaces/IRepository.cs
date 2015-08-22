namespace com.kiransprojects.travelme.DataAccess.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;

    /// <summary>
    /// Repository interface to be used for RepositoryBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Inserts a record into the database of type T
        /// </summary>
        /// <param name="Entity"></param>
        void Insert(T Entity);

        /// <summary>
        /// Gets entity of type T with the Guid ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        T GetByID(Guid ID);

        /// <summary>
        /// Updates Entity of type T
        /// </summary>
        /// <param name="Entity"></param>
        void Update(T Entity); 

        /// <summary>
        /// Deletes Entity of type T
        /// </summary>
        /// <param name="Entity"></param>
        void Delete(T Entity); 
    }
}