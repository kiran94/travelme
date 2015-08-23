namespace com.kiransprojects.travelme.DataAccess
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using NHibernate;
    using System; 

    /// <summary>
    /// Nhibernate Helper
    /// </summary>
    public class NhibernateHelper : INhibernateHelper
    {
        /// <summary>
        /// Session Factory
        /// </summary>
        private ISessionFactory _sessionFactory = null;

        /// <summary>
        /// Database Configuration
        /// </summary>
        private readonly IDatabaseConfig config = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="NhibernateHelper"/> class.
        /// </summary>
        /// <param name="config"></param>
        public NhibernateHelper(IDatabaseConfig config)
        {
            if (config == null)
            {
                throw new NullReferenceException("IDatabaseConfig"); 
            }
            
            this.config = config; 
        }

        /// <summary>
        /// Gets or sets Session Factory
        /// </summary>
        private ISessionFactory SessionFactory
        {
            get
            {
                if (this._sessionFactory == null)
                {
                    _sessionFactory = config.GetConfig().BuildSessionFactory(); 
                }
                return this._sessionFactory; 
            }
        }

        /// <summary>
        /// Gets Session
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return this.SessionFactory.OpenSession(); 
        }
    }
}