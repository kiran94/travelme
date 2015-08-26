namespace com.kiransprojects.travelme.DataAccess.Tests.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using Moq;
    using NHibernate.Cfg;
    using NHibernate.Cfg.MappingSchema;
    using NHibernate.Dialect;
    using NHibernate.Driver;
    using NHibernate.Mapping.ByCode;
    using NUnit.Framework;
    using System.Reflection;

    /// <summary>
    /// Trip Repository Tests
    /// </summary>
    [TestFixture]
    public class TripRepositoryTests
    {
        /// <summary>
        /// Nhibernate Helper
        /// </summary>
        private NhibernateHelper helper = null;

        /// <summary>
        /// Mocking Of Configuration class
        /// </summary>
        private Mock<IDatabaseConfig> MockConfig = null; 

        /// <summary>
        /// Run before each test
        /// </summary>
        [SetUp]
        public void SetUp()
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

            this.MockConfig = new Mock<IDatabaseConfig>();
            this.MockConfig.SetupSequence(o => o.GetConfig()).Returns(config);
        }
    }
}