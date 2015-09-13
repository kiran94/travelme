namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Services;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// LoginService Tests
    /// </summary>
    [TestFixture]
    public class LoginServiceTests
    {
        /// <summary>
        /// Ensures a valid user can be registered
        /// </summary>
        [Test]
        public void RegisterUser_ValidUser_Registered()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            repository.Setup(o => o.Insert(It.IsAny<UserEntity>())); 

            LoginService service = new LoginService(repository.Object);

            UserEntity entity = new UserEntity();

            entity = service.RegisterUser(entity);

            //Assert.IsTrue(flag);
        }


 







        /// <summary>
        /// Ensures true is returned when non empty strings are passed 
        /// </summary>
        [Test]
        public void SignIn_CorrectDetails_ReturnTrue()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();

            string Role; 
            repository.Setup(o => o.Authenticate(It.IsAny<string>(), It.IsAny<string>(), out Role)).Returns(true);

            LoginService service = new LoginService(repository.Object);
            bool flag = service.SignIn("test@test.com", "123", out Role);

            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Ensures false is returned when email is null
        /// </summary>
        [Test]
        public void SignIn_NullEmail_ReturnFalse()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();

            string Role;
            repository.Setup(o => o.Authenticate(It.IsAny<string>(), It.IsAny<string>(), out Role)).Returns(true);

            LoginService service = new LoginService(repository.Object);
            bool flag = service.SignIn(null, "123", out Role);

            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures false is returned when password is null
        /// </summary>
        [Test]
        public void SignIn_NullPassword_ReturnFalse()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();

            string Role;
            repository.Setup(o => o.Authenticate(It.IsAny<string>(), It.IsAny<string>(), out Role)).Returns(true);

            LoginService service = new LoginService(repository.Object);
            bool flag = service.SignIn("test@test.com", null, out Role);

            Assert.IsFalse(flag);
        }
    }
}