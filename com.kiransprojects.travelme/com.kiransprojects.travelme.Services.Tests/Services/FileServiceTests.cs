namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.Services.Services;
    using NUnit.Framework;
    using System; 

    /// <summary>
    /// File Service Tests
    /// </summary>
    [TestFixture]
    public class FileServiceTests
    {
        private string StoredMedia = Environment.CurrentDirectory +  "/test.txt";
        private byte[] data = new byte[1];

        [SetUp]
        public void setup()
        {
            FileService Service = new FileService();
            Service.SaveMedia(StoredMedia, data); 
        }

        /// <summary>
        /// Ensures media can be saved
        /// </summary>
        [Test]
        public void SaveMedia_NormalCase_Saved()
        {
            FileService Service = new FileService();
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
            FileService Service = new FileService();
            byte[] retrieved = Service.GetMedia(this.StoredMedia);
            Assert.AreEqual("System.Byte[]", retrieved.ToString()); 
        }

        /// <summary>
        /// Ensures media can be deleted
        /// </summary>
        [Test]
        public void DeleteMedia_NormalCase_Deleted()
        {
            FileService Service = new FileService();
            bool flag = Service.DeleteMedia(this.StoredMedia);
            Assert.IsTrue(flag); 
        }
    }
}