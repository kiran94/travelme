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
    /// User Service Tests
    /// </summary>
    [TestFixture]
    public class UserServiceTests
    {
        /// <summary>
        /// Ensures property is set when repository is injected
        /// </summary>
        [Test]
        public void Constructor_RepositoryInjected_PropertySet()
        {
            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Mock<IFileService> FileService = new Mock<IFileService>();
            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>(); 

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            Assert.IsTrue(Service.isRepositorySet());
        }

        /// <summary>
        /// Ensures property exception is thrown when null is injected
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor_RepositoryNull_ExceptionThrown()
        {
            Mock<IFileService> FileService = new Mock<IFileService>();
            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(null, FileService.Object, LoggerService.Object);
        }

        /// <summary>
        /// Ensures exception is thrown when file service is null
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor_FileServiceNull_ExceptionThrown()
        {
            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, null, LoggerService.Object);
        }

        /// <summary>
        /// Ensures property exception is thrown when null is injected
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor_LoggerNull_ExceptionThrown()
        {
            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Mock<IFileService> FileService = new Mock<IFileService>();

            UserService Service = new UserService(null, FileService.Object, null);
        }

        /// <summary>
        /// Ensures user is retrieved when user exists
        /// </summary>
        [Test]
        public void GetUser_ExistingUser_Retrieved()
        {
            UserEntity User = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran",
                LastName = "Patel"
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.SetupSequence(o => o.GetByID(User.ID)).Returns(User);

            Mock<IFileService> FileService = new Mock<IFileService>();
            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            UserEntity Retrieved = Service.GetUser(User.ID);
            Assert.AreEqual(User, Retrieved);
        }

        /// <summary>
        /// Ensures null is retrieved when user does not exist
        /// </summary>
        [Test]
        public void GetUser_NonExistingUser_NullRetrieved()
        {
            Guid ID = Guid.NewGuid();
            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.SetupSequence(o => o.GetByID(ID)).Returns(null);

            Mock<IFileService> FileService = new Mock<IFileService>();
            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            UserEntity Retrieved = Service.GetUser(ID);
            Assert.IsNull(Retrieved);
        }

        /// <summary>
        /// Ensures a profile picture can be added to a user
        /// </summary>
        [Test]
        public void AddProfilePicture_ExistingUser_PathAdded()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran"
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.Setup(o => o.Update(Entity, false));
            Repository.Setup(o => o.GetByID(Entity.ID)).Returns(Entity);

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            string path = Service.AddProfilePicture(Entity.ID, Environment.CurrentDirectory, new byte[1]);

            string expected = string.Format("{0}/Profile{1}.jpg", Environment.CurrentDirectory, Entity.ID.ToString());
            Assert.AreEqual(expected, path);
        }

        /// <summary>
        /// Ensures a profile picture can be added to a user
        /// </summary>
        [Test]
        public void AddProfilePicture_NonExisting_EmptyPath()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran"
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.Setup(o => o.Update(Entity, false));
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(null);

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            string path = Service.AddProfilePicture(Entity.ID, Environment.CurrentDirectory, new byte[1]);

            Assert.AreEqual(string.Empty, path);
        }

        /// <summary>
        /// Ensures existing users profile picture can be edited
        /// </summary>
        [Test]
        public void EditProfilePicture_Existing_Edited()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran",
                ProfilePicture = "Path/ProfilePicture.jpg"
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.SetupSequence(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            bool flag = Service.EditProfilePicture(Entity.ID, new byte[2]);

            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Ensures non existing users profile picture is false
        /// </summary>
        [Test]
        public void EditProfilePicture_NonExisting_False()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran",
                ProfilePicture = "Path/ProfilePicture.jpg"
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(null);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.SetupSequence(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            bool flag = Service.EditProfilePicture(Entity.ID, new byte[2]);

            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures existing users profile picture with empty profile properties returns false
        /// </summary>
        [Test]
        public void EditProfilePicture_EmptyProfilePicture_False()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran",
                ProfilePicture = string.Empty
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.SetupSequence(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            bool flag = Service.EditProfilePicture(Entity.ID, new byte[2]);

            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures existing users profile picture with null profile properties returns false
        /// </summary>
        [Test]
        public void EditProfilePicture_NullProfilePicture_False()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran",
                ProfilePicture = null
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.SetupSequence(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            bool flag = Service.EditProfilePicture(Entity.ID, new byte[2]);

            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures Profile Pictures can be removed
        /// </summary>
        [Test]
        public void RemoveProfilePicture_Existing_True()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran",
                ProfilePicture = "OldProfilepicture/pic.jpg"
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.DeleteMedia(It.IsAny<string>())).Returns(true);

            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            bool flag = Service.RemoveProfilePicture(Entity.ID);

            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Ensures a non existing user returns false
        /// </summary>
        [Test]
        public void RemoveProfilePicture_NonExisting_False()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran",
                ProfilePicture = "OldProfilepicture/pic.jpg"
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(null);
            Repository.Setup(o => o.Update(null, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.DeleteMedia(It.IsAny<string>())).Returns(true);

            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            UserService Service = new UserService(Repository.Object, FileService.Object, LoggerService.Object);
            bool flag = Service.RemoveProfilePicture(Entity.ID);

            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures a null path returns false
        /// </summary>
        [Test]
        public void RemoveProfilePicture_NullPath_False()
        {
            UserEntity Entity = new UserEntity()
            {
                ID = Guid.NewGuid(),
                FirstName = "Kiran",
                ProfilePicture = string.Empty
            };

            Mock<IUserEntityRepository> Repository = new Mock<IUserEntityRepository>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.DeleteMedia(It.IsAny<string>())).Returns(true);
            Mock<ILoggerService> LoggerService = new Mock<ILoggerService>();

            bool flag = new UserService(Repository.Object, FileService.Object, LoggerService.Object).RemoveProfilePicture(Entity.ID);

            Assert.IsFalse(flag); 
        }

        /// <summary>
        /// Ensures a user can be added
        /// </summary>
        [Test]
        public void AddUser_ValidUser_True()
        {
            UserEntity user = new UserEntity();
            user.ID = Guid.NewGuid();
            user.FirstName = "test";

            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IFileService> service = new Mock<IFileService>();
            Mock<ILoggerService> logger = new Mock<ILoggerService>(); 

            UserService userService = new UserService(repository.Object, service.Object, logger.Object);

            bool flag = userService.AddUser(user);

            Assert.IsTrue(flag); 
        }
    }
}