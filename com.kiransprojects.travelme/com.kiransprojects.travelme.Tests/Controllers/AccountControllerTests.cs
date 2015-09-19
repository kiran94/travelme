namespace com.kiransprojects.travelme.Tests.Controllers
{
    using com.kiransprojects.travelme.Controllers;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Models;
    using com.kiransprojects.travelme.Services.Interfaces;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Account Controller Tests
    /// </summary>
    [TestFixture]
    public class AccountControllerTests
    {
        /// <summary>
        /// Ensures when the user model passes they are authenticated
        /// </summary>
        [Test]
        public void Login_ValidCredentials_ReturnsValidView()
        {
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<ILoginService> loginService = new Mock<ILoginService>();
            Mock<IUserService> userService = new Mock<IUserService>(); 

            AccountController controller = new AccountController(
                loggerService.Object,
                loginService.Object,
                userService.Object);

            UserViewModel model = new UserViewModel(); 
            
            UserEntity user = new UserEntity();
            user.ID = Guid.NewGuid();
            user.Email = "test@test.com";
            user.UserPassword = "test";

            model.User = user;

            ActionResult actionResult = controller.Login(model);
            
        }

        /// <summary>
        /// Ensures when the user model passes null it is handled
        /// </summary>
        [Test]
        public void Login_NullUser_ReturnsLoginView()
        {
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<ILoginService> loginService = new Mock<ILoginService>();
            Mock<IUserService> userService = new Mock<IUserService>();

            AccountController controller = new AccountController(
                loggerService.Object,
                loginService.Object,
                userService.Object);

            ActionResult actionResult = controller.Login(null);
        }

        /// <summary>
        /// Ensures when the user model passes empty email and password it is handled
        /// </summary>
        [Test]
        public void Login_EmptyCredentials_ReturnsLoginView()
        {
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();
            Mock<ILoginService> loginService = new Mock<ILoginService>();
            Mock<IUserService> userService = new Mock<IUserService>();

            AccountController controller = new AccountController(
                loggerService.Object,
                loginService.Object,
                userService.Object);

            UserViewModel model = new UserViewModel(); 

            UserEntity user = new UserEntity();
            user.ID = Guid.NewGuid();
            user.Email = string.Empty;
            user.UserPassword = string.Empty;

            model.User = user;


            ActionResult actionResult = controller.Login(model);
        }

    }
}