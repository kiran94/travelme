namespace com.kiransprojects.travelme.Tests.Controllers
{
    using com.kiransprojects.travelme.Controllers;
    using com.kiransprojects.travelme.Services.Interfaces;
    using Moq;
    using NUnit.Framework;
    using System;

    /// <summary>
    /// BaseController Tests
    /// </summary>
    [TestFixture]
    public class BaseControllerTests
    {
        /// <summary>
        /// Ensures an exception is thrown when the user service is null
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contructor_NullUserService_ExceptionThrown()
        {
            //Mock<IUserService> userService = new Mock<IUserService>();
            Mock<ILoginService> loginService = new Mock<ILoginService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            BaseController controller = new BaseController(null, loginService.Object, loggerService.Object);
        }

        /// <summary>
        /// Ensures an exception is thrown when the login service is null
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contructor_NullLoginService_ExceptionThrown()
        {
            Mock<IUserService> userService = new Mock<IUserService>();
            //Mock<ILoginService> loginService = new Mock<ILoginService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            BaseController controller = new BaseController(userService.Object, null, loggerService.Object);
        }

        /// <summary>
        /// Ensures an exception is thrown when the logger service is null
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Contructor_NullLoggerService_ExceptionThrown()
        {
            Mock<IUserService> userService = new Mock<IUserService>();
            Mock<ILoginService> loginService = new Mock<ILoginService>();
            //Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            BaseController controller = new BaseController(userService.Object, loginService.Object, null);
        }
    }
}