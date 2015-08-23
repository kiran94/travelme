namespace com.kiransprojects.travelme.DataAccess
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using NHibernate.Cfg;
    using NHibernate.Cfg.MappingSchema;
    using NHibernate.Dialect;
    using NHibernate.Driver;
    using NHibernate.Mapping.ByCode;
    using System.Reflection; 

    /// <summary>
    /// Configuration Singleton
    /// </summary>
    public class NhibernateConfigurationSingleton : IDatabaseConfig
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public static Configuration _config = null; 

        /// <summary>
        /// Stops class from being initialised
        /// </summary>
        public NhibernateConfigurationSingleton()
        {
            GetConfig(); 
        }

        /// <summary>
        /// Generates the configuration
        /// </summary>
        /// <returns></returns>
        private Configuration GenerateConfig()
        {
            Configuration config = new Configuration();
            config.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
                db.ConnectionString = "Data Source=DESKTOP-0II3UCP\\MAINSERVER;Initial Catalog=travelme;Integrated Security=True";
            });

            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            config.AddMapping(mapping);

            return config; 
        }
        
        /// <summary>
        /// Gets the configuration if not already taken
        /// </summary>
        /// <returns>Nhibenate Configuration Object</returns>
        public Configuration GetConfig()
        {
            if(_config == null)
            {
                _config = GenerateConfig();
            }
            return _config; 
        }
    }   
}