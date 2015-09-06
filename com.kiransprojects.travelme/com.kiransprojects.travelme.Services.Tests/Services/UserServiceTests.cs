namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using NUnit.Framework;
    using Moq; 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Service;
    using com.kiransprojects.travelme.Services.Interfaces; 
    
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
        public void Constructor_FileServiceNull_ExceptionThrown()
        {
            Mock<IRepository<UserEntity>> Repository = new Mock<IRepository<UserEntity>>();
            UserService Service = new UserService(Repository.Object, null);
        }

        /// <summary>
        /// Ensures user is retrieved when user exists
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
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
            string path = Service.AddProfilePicture(Entity.ID, new byte[1]);

            string expected = string.Format("Profile{0}.jpg", Entity.ID.ToString());
            Assert.AreEqual(expected, path); 
        }

        
    }
}
