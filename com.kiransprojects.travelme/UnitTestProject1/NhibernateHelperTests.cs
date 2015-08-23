namespace com.kiransprojects.travelme.DataAccess.Tests
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using Moq;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Dialect;
    using NHibernate.Driver;
    using NUnit.Framework;
    using System; 

    [TestFixture]
    public class NhibernateHelperTests
    {
        /// <summary>
        /// Mock for IDatabaseConfig
        /// </summary>
        private Mock<IDatabaseConfig> MockConfig = null; 

        /// <summary>
        /// SetUp function for each test
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

            this.MockConfig = new Mock<IDatabaseConfig>();
            this.MockConfig.SetupSequence(o => o.GetConfig()).Returns(config); 
        }

        /// <summary>
        /// Ensures a session is returned from session factory
        /// </summary>
        [Test]
        public void GetSession_NormalCase_ReturnsSession()
        {
            NhibernateHelper helper = new NhibernateHelper(this.MockConfig.Object);
            ISession session = helper.GetSession();
            Assert.AreEqual(typeof(NHibernate.Impl.SessionImpl), session.GetType());
        }

        /// <summary>
        /// Ensures an exception is thrown when null is passed
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetSession_NullConfig_ThrowsNullReferenceException()
        {
            NhibernateHelper helper = new NhibernateHelper(null);
            helper.GetSession(); 
        }
    }
}