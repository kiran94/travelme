namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using Moq;    

    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Service;
    using com.kiransprojects.travelme.Services.Interfaces;

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
            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Mock<IFileService> FileService = new Mock<IFileService>();

            UserService Service = new UserService(Repository.Object, FileService.Object);
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
            UserService Service = new UserService(null, FileService.Object);
        }

        /// <summary>
        /// Ensures exception is thrown when file service is null
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor_FileServiceNull_ExceptionThrown()
        {
            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            UserService Service = new UserService(Repository.Object, null);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.SetupSequence(o => o.GetByID(User.ID)).Returns(User);

            Mock<IFileService> FileService = new Mock<IFileService>();

            UserService Service = new UserService(Repository.Object, FileService.Object);
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
            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.SetupSequence(o => o.GetByID(ID)).Returns(null);

            Mock<IFileService> FileService = new Mock<IFileService>();

            UserService Service = new UserService(Repository.Object, FileService.Object);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.Setup(o => o.Update(Entity, false));
            Repository.Setup(o => o.GetByID(Entity.ID)).Returns(Entity);

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            UserService Service = new UserService(Repository.Object, FileService.Object);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.Setup(o => o.Update(Entity, false));
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(null);

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            UserService Service = new UserService(Repository.Object, FileService.Object);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.SetupSequence(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            UserService Service = new UserService(Repository.Object, FileService.Object);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(null);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.SetupSequence(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            UserService Service = new UserService(Repository.Object, FileService.Object);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.SetupSequence(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            UserService Service = new UserService(Repository.Object, FileService.Object);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.SetupSequence(o => o.SaveMedia(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);

            UserService Service = new UserService(Repository.Object, FileService.Object);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.DeleteMedia(It.IsAny<string>())).Returns(true);

            UserService Service = new UserService(Repository.Object, FileService.Object);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(null);
            Repository.Setup(o => o.Update(null, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.DeleteMedia(It.IsAny<string>())).Returns(true);

            UserService Service = new UserService(Repository.Object, FileService.Object);
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

            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            Repository.SetupSequence(o => o.GetByID(Entity.ID)).Returns(Entity);
            Repository.Setup(o => o.Update(Entity, false));

            Mock<IFileService> FileService = new Mock<IFileService>();
            FileService.Setup(o => o.DeleteMedia(It.IsAny<string>())).Returns(true);

            bool flag = new UserService(Repository.Object, FileService.Object).RemoveProfilePicture(Entity.ID);

            Assert.IsFalse(flag); 
        }
    }
}