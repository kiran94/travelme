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
    using System.Reflection;

    [TestFixture]
    public class UserEntityRespositoryTests
    {
        private Mock<IDatabaseConfig> MockConfig = null;
        private NhibernateHelper helper = null; 

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
            
            //config.AddAssembly(Assembly.Load("com.kiransprojects.travelme.DataAccess.Mappings")); 
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            config.AddMapping(mapping); 
           
            MockConfig = new Mock<IDatabaseConfig>();
            MockConfig.SetupSequence(o => o.GetConfig()).Returns(config);

            helper = new NhibernateHelper(MockConfig.Object);
        }

        /// <summary>
        /// Ensures that an existing entity in the database can be retrieved
        /// </summary>
        [Test]
        public void GetByID_ExistingEntity_RetrieveEntity()
        {
            UserEntity entity = new UserEntity()
            {
                ID = Guid.Parse("9fc0b724-d55f-441d-a1ae-ec726d7737f7"),
                FirstName = "TestFname",
                LastName = "TestLName",
                DateOfBirth = new DateTime(1994, 08, 05, 10, 00, 00),
                Email = "test@test.com",
                UserPassword = "password"
            };

            UserEntityRepository Repository = new UserEntityRepository(helper);
            UserEntity Returned = Repository.GetByID(entity.ID);
          
            Assert.AreEqual(entity.ID, Returned.ID);
            Assert.AreEqual(entity.FirstName, Returned.FirstName);
            Assert.AreEqual(entity.LastName, Returned.LastName);
            Assert.AreEqual(entity.DateOfBirth, Returned.DateOfBirth);
            Assert.AreEqual(entity.Email, Returned.Email);
            Assert.AreEqual(entity.UserPassword, Returned.UserPassword);
        }
    }
}