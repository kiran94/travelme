namespace com.kiransprojects.travelme.DataAccess.Tests.Repositories
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.DataAccess.Repositories;
    using com.kiransprojects.travelme.Framework.Entities;
    using Moq;
    using NHibernate.Cfg;
    using NHibernate.Cfg.MappingSchema;
    using NHibernate.Dialect;
    using NHibernate.Driver;
    using NHibernate.Mapping.ByCode;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
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
        /// Trip Test Data
        /// </summary>
        private Trip TestTrip;

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

            this.helper = new NhibernateHelper(this.MockConfig.Object);

            TestTrip = new Trip()
            {
                ID = Guid.Parse("6D8BCE5C-5681-472E-A1DC-97C5EA0C23FA"),
                TripName = "Thailand",
                TripDescription = "Thai!",
                TripLocation = "Backpacking",
                RelationID = Guid.Parse("9fc0b724-d55f-441d-a1ae-ec726d7737f7")
            };
        }

        /// <summary>
        /// Ensures trip can be retrieved 
        /// </summary>
        [Test]
        public void GetByID_ExistingEntity_Retrieved()
        {
            TripRepository Repository = new TripRepository(helper);
            Assert.AreEqual(TestTrip.ID, Repository.GetByID(TestTrip.ID).ID);
        }

        /// <summary>
        /// Ensures exception is raised when value is null
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetByID_NonExistingEntity_Retrieved()
        {
            TripRepository Repository = new TripRepository(helper);
            Assert.AreEqual(null, Repository.GetByID(Guid.NewGuid()).ID);
        }

        


    }
}