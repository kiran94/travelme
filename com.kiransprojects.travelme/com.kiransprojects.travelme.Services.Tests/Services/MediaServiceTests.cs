namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using com.kiransprojects.travelme.Services.Services;
    using Moq;
    using NUnit.Framework;
    using System;

    /// <summary>
    /// Media Service Tests
    /// </summary>
    [TestFixture]
    public class MediaServiceTests
    {
        /// <summary>
        /// Ensures media can be retrieved 
        /// </summary>
        [Test]
        public void GetMedia_ValidMedia_ReturnsMedia()
        {
            Media media = new Media()
            {
                ID = Guid.NewGuid(),
                MediaData = "test",
                RelationID = Guid.NewGuid()
            };

            Mock<IRepository<Media>> repository = new Mock<IRepository<Media>>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>(); 
            repository.Setup(o => o.GetByID(It.IsAny<Guid>())).Returns(media);

            MediaService service = new MediaService(repository.Object, loggerService.Object);
            Media retrieved = service.GetMedia(Guid.NewGuid());

            Assert.NotNull(retrieved);
        }

    }
}