namespace com.kiransprojects.travelme.DataAccess.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Repository Base 
    /// </summary>
    /// <typeparam name="T">Template Type</typeparam>
    public class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Nhibernate Helper
        /// </summary>
        protected readonly INhibernateHelper helper = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        public RepositoryBase(INhibernateHelper helper)
        {
            if (helper == null)
            {
                throw new NullReferenceException("INhibernateHelper");
            }
            this.helper = helper;
        }

        /// <inheritdoc />
        public void Insert(T Entity)
        {
            using (ISession session = this.helper.GetSession())
            {
                using (ITransaction transactions = session.BeginTransaction())
                {
                    session.Save(Entity);
                    transactions.Commit();
                    session.Flush();
                }
            }
        }

        /// <inheritdoc />
        public T GetByID(Guid ID)
        {
            T Entity = null;
            using (ISession session = this.helper.GetSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Entity = session.Get<T>(ID);
                    transaction.Commit();
                    session.Flush();

                }
            }
            return Entity;
        }

        /// <inheritdoc />
        public void Update(T Entity, bool load)
        {
            using (ISession session = this.helper.GetSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    T DatabaseVersion;
                    if (load)
                    {
                        DatabaseVersion = session.Load<T>(Entity.ID);
                    }
                    
                    DatabaseVersion = Entity;

                    session.Merge(DatabaseVersion);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        /// <inheritdoc />
        public void Delete(T Entity)
        {
            using (ISession session = this.helper.GetSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(Entity);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        /// <inheritdoc />
        public IList<T> GetAll()
        {
            IList<T> List = null; 
            using(ISession session = this.helper.GetSession())
            {
                using(ITransaction transaction = session.BeginTransaction())
                {
                    List = session.CreateCriteria<T>().List<T>(); 
                    transaction.Commit();
                    session.Flush(); 
                }
            }

            return List; 
        }
    }
}