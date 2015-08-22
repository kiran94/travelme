namespace com.kiransprojects.travelme.DataAccess
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    using NHibernate.Driver; 

    /// <summary>
    /// Configuration factory
    /// </summary>
    public class NhibernateConfigurationFactory : IDatabaseConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NhibernateConfigurationFactory"/> class.
        /// </summary>
        public NhibernateConfigurationFactory()
        {

        }
        
        /// <summary>
        /// Gets the configuration 
        /// </summary>
        /// <returns>Nhibenate Configuration Object</returns>
        public Configuration GetConfig()
        {
            Configuration config = new Configuration();
            config.DataBaseIntegration(db =>
                {
                    db.Dialect<MsSql2012Dialect>();
                    db.Driver<SqlClientDriver>();
                    db.ConnectionString = "Data Source=DESKTOP-0II3UCP\\MAINSERVER;Initial Catalog=travelme;Integrated Security=True"; 
                });

            return config; 
        }
    }   
}