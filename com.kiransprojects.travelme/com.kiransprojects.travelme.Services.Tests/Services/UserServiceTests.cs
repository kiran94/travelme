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
            UserService Service = new UserService(Repository.Object);

            Assert.IsTrue(Service.isRepositorySet()); 
        }

        /// <summary>
        /// Ensures property exception is thrown when null is injected
        /// </summary>
        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor_RepositoryNull_ExceptionThrown()
        {
            UserService Service = new UserService(null);
            
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

            UserService Service = new UserService(Repository.Object);
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

            UserService Service = new UserService(Repository.Object);
            UserEntity Retrieved = Service.GetUser(ID); 
            Assert.IsNull(Retrieved);
        }
    }
}
