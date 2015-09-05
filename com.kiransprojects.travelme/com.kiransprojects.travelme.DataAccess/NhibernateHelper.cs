namespace com.kiransprojects.travelme.DataAccess
{
    using System;
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using NHibernate;

    /// <summary>
    /// Nhibernate Helper
    /// </summary>
    public class NhibernateHelper : INhibernateHelper
    {
        /// <summary>
        /// Database Configuration
        /// </summary>
        private readonly IDatabaseConfig config = null;

        /// <summary>
        /// Session Factory
        /// </summary>
        private ISessionFactory _sessionFactory = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="NhibernateHelper"/> class.
        /// </summary>
        /// <param name="config">Configuration dependacy injection</param>
        public NhibernateHelper(IDatabaseConfig config)
        {
            if (config == null)
            {
                throw new NullReferenceException("IDatabaseConfig");
            }

            this.config = config;
        }

        /// <summary>
        /// Gets the session factory for the application, initialize if null
        /// </summary>
        /// <returns>Returns the ISessionFactory</returns>
        private ISessionFactory SessionFactory
        {
            get
            {
                if (this._sessionFactory == null)
                {
                    this._sessionFactory = this.config.GetConfig().BuildSessionFactory();
                }

                return this._sessionFactory;
            }
        }

        /// <summary>
        /// Opens a session and returns
        /// </summary>
        /// <returns>ISession to be used in database transactions</returns>
        public ISession GetSession()
        {
            return this.SessionFactory.OpenSession();
        }
    }
}