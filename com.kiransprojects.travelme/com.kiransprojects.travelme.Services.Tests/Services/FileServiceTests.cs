namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using com.kiransprojects.travelme.Services.Services;
    using Moq;
    using NUnit.Framework;
    using System; 

    /// <summary>
    /// File Service Tests
    /// </summary>
    [TestFixture]
    public class FileServiceTests
    {
        /// <summary>
        /// Test Media
        /// </summary>
        private string StoredMedia = Environment.CurrentDirectory +  "/test.txt";

        /// <summary>
        /// Byte Array
        /// </summary>
        private byte[] data = new byte[1];

        /// <summary>
        /// Setup method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            loggerService.SetupSequence(o => o.Log(It.IsAny<Log>())).Returns(true);

            FileService Service = new FileService(loggerService.Object);
            Service.SaveMedia(StoredMedia, data); 
        }

        /// <summary>
        /// Ensures media can be saved
        /// </summary>
        [Test]
        public void SaveMedia_NormalCase_Saved()
        {
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            loggerService.SetupSequence(o => o.Log(It.IsAny<Log>())).Returns(true);

            FileService Service = new FileService(loggerService.Object);
            string path = "/test.txt";
            bool flag = Service.SaveMedia(path, new byte[1]);
            Assert.IsTrue(flag); 
        }

        /// <summary>
        /// Ensures media can be retrieved
        /// </summary>
        [Test]
        public void GetMedia_NormalCase_Saved()
        {
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            loggerService.SetupSequence(o => o.Log(It.IsAny<Log>())).Returns(true);

            FileService Service = new FileService(loggerService.Object);

            byte[] retrieved = Service.GetMedia(this.StoredMedia);
            Assert.AreEqual("System.Byte[]", retrieved.ToString()); 
        }

        /// <summary>
        /// Ensures media can be deleted
        /// </summary>
        [Test]
        public void DeleteMedia_NormalCase_Deleted()
        {
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            loggerService.SetupSequence(o => o.Log(It.IsAny<Log>())).Returns(true);

            FileService Service = new FileService(loggerService.Object);
            bool flag = Service.DeleteMedia(this.StoredMedia);
            Assert.IsTrue(flag); 
        }
    }
}