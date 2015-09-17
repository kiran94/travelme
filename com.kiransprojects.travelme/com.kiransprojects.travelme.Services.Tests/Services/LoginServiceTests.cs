namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using com.kiransprojects.travelme.Services.Services;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

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
            UserEntity entity = new UserEntity();
            entity.ID = Guid.NewGuid();
            entity.FirstName = "Test";
            entity.UserPassword = "123sadojasdDDIAD";
            entity.Email = "test@test.com";

            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>(); 

            repository.Setup(o => o.Insert(It.IsAny<UserEntity>()));
            repository.Setup(o => o.isEmailInUse(It.IsAny<string>())).Returns(false);
            passwordService.SetupSequence(o => o.GenerateCredentials(entity)).Returns(entity);
            mailService.SetupSequence(o => o.SendMessage(
                It.IsAny<System.Collections.Generic.List<string>>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<bool>()))
                .Returns(true);

            LoginService service = new LoginService(
                                        repository.Object, 
                                        passwordService.Object, 
                                        mailService.Object, 
                                        loggerService.Object);

            entity = service.RegisterUser(entity);

            Assert.IsNotNull(entity); 
        }

        /// <summary>
        /// Ensures null is returned when email is in use
        /// </summary>
        [Test]
        public void RegisterUser_EmailInUse_NotRegistered()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>(); 

            repository.Setup(o => o.isEmailInUse(It.IsAny<string>())).Returns(true);

            LoginService service = new LoginService(
                                      repository.Object,
                                      passwordService.Object,
                                      mailService.Object,
                                      loggerService.Object);

            UserEntity user = new UserEntity();
            user.ID = Guid.NewGuid(); 
            user.FirstName = "Test"; 
            user.UserPassword = "123sadojasdDDIAD";
            user.Email = "test@test.com";

            user = service.RegisterUser(user);
            Assert.IsNull(user);
        }

        /// <summary>
        /// Ensures null is returned when an empty password is passed
        /// </summary>
        [Test]
        public void RegisterUser_EmptyPassword_NotRegistered()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>(); 

            repository.Setup(o => o.isEmailInUse(It.IsAny<string>())).Returns(false); 
            passwordService.SetupSequence(o => o.GenerateCredentials(It.IsAny<UserEntity>())).Returns(null);

            LoginService service = new LoginService(
                                       repository.Object,
                                       passwordService.Object,
                                       mailService.Object,
                                       loggerService.Object);

            UserEntity user = new UserEntity();
            user.ID = Guid.NewGuid(); 
            user.FirstName = "Test"; 
            user.UserPassword = string.Empty;
            user.Email = "test@test.com";

            UserEntity retuser = service.RegisterUser(user);
            Assert.IsNull(retuser);
        }

        /// <summary>
        /// Ensures null is returned when user is null
        /// </summary>
        [Test]
        public void RegisterUser_NullUser_NotRegistered()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            LoginService service = new LoginService(
                                      repository.Object,
                                      passwordService.Object,
                                      mailService.Object,
                                      loggerService.Object);
   
            UserEntity retuser = service.RegisterUser(null);
            Assert.IsNull(retuser);
        }

        /// <summary>
        /// Ensures true is returned when non empty strings are passed 
        /// </summary>
        [Test]
        public void SignIn_CorrectDetails_ReturnTrue()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>(); 

            string Role; 
            repository.Setup(o => o.Authenticate(It.IsAny<string>(), It.IsAny<string>(), out Role)).Returns(true);

            LoginService service = new LoginService(
                                      repository.Object,
                                      passwordService.Object,
                                      mailService.Object,
                                      loggerService.Object);

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
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>(); 

            string Role;
            repository.Setup(o => o.Authenticate(It.IsAny<string>(), It.IsAny<string>(), out Role)).Returns(true);

            LoginService service = new LoginService(
                                       repository.Object,
                                       passwordService.Object,
                                       mailService.Object,
                                       loggerService.Object);

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
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>(); 

            string Role;
            repository.Setup(o => o.Authenticate(It.IsAny<string>(), It.IsAny<string>(), out Role)).Returns(true);

            LoginService service = new LoginService(
                                      repository.Object,
                                      passwordService.Object,
                                      mailService.Object,
                                      loggerService.Object);

            bool flag = service.SignIn("test@test.com", null, out Role);

            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures false is returned when the wrong credentials are sent
        /// </summary>
        [Test]
        public void SignIn_WrongCredentails_False()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            string Role;
            repository.Setup(o => o.Authenticate(It.IsAny<string>(), It.IsAny<string>(), out Role)).Returns(false);
            repository.Setup(o => o.Update(It.IsAny<UserEntity>(), false));


            UserEntity user = new UserEntity(); 
            user.ID = Guid.NewGuid(); 
            user.FirstName = "Test";

            repository.Setup(o => o.GetByEmail(It.IsAny<string>())).Returns(user);

            LoginService service = new LoginService(
                                      repository.Object,
                                      passwordService.Object,
                                      mailService.Object,
                                      loggerService.Object);

            bool flag = service.SignIn("test@test.com", "123", out Role);

            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures false is returned when the wrong credentials with existing count are sent
        /// </summary>
        [Test]
        public void SignIn_WrongCredentailsWithExistingCount_False()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            string Role;
            repository.Setup(o => o.Authenticate(It.IsAny<string>(), It.IsAny<string>(), out Role)).Returns(false);
            repository.Setup(o => o.Update(It.IsAny<UserEntity>(), false));


            UserEntity user = new UserEntity();
            user.ID = Guid.NewGuid();
            user.FirstName = "Test";
            user.InvalidPasswordCount = 1; 

            repository.Setup(o => o.GetByEmail(It.IsAny<string>())).Returns(user);

            LoginService service = new LoginService(
                                      repository.Object,
                                      passwordService.Object,
                                      mailService.Object,
                                      loggerService.Object);

            bool flag = service.SignIn("test@test.com", "123", out Role);

            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures when an email is sent, email is sent out
        /// </summary>
        [Test]
        public void ForgotPassword_NormalEmail_True()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            mailService.Setup(o => o.SendMessage(It.IsAny<List<string>>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),It.IsAny<bool>())).Returns(true);

            UserEntity user = new UserEntity(); 
            user.ID = Guid.NewGuid(); 
            user.FirstName = "Test";

            repository.Setup(o => o.GetByEmail(It.IsAny<string>())).Returns(user);
            loggerService.Setup(o => o.Log(It.IsAny<Log>()));

            LoginService service = new LoginService(
                                  repository.Object,
                                  passwordService.Object,
                                  mailService.Object,
                                  loggerService.Object);

            bool flag = service.ForgotPassword("test@test.com");
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Ensures when an email is sent, email is sent out
        /// </summary>
        [Test]
        public void ForgotPassword_NonExistingUser_True()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            repository.SetupSequence(o => o.GetByEmail(It.IsAny<string>())).Returns(null);

            LoginService service = new LoginService(
                                  repository.Object,
                                  passwordService.Object,
                                  mailService.Object,
                                  loggerService.Object);

            bool flag = service.ForgotPassword("test@test.com");
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures false is returned when email is stringempty
        /// </summary>
        [Test]
        public void ForgotPassword_stringEmpty_False()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            LoginService service = new LoginService(
                                  repository.Object,
                                  passwordService.Object,
                                  mailService.Object,
                                  loggerService.Object);

            bool flag = service.ForgotPassword(string.Empty);
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures false is returned when email is null
        /// </summary>
        [Test]
        public void ForgotPassword_null_False()
        {
            Mock<IUserEntityRepository> repository = new Mock<IUserEntityRepository>();
            Mock<IPasswordService> passwordService = new Mock<IPasswordService>();
            Mock<IMailService> mailService = new Mock<IMailService>();
            Mock<ILoggerService> loggerService = new Mock<ILoggerService>();

            LoginService service = new LoginService(
                                  repository.Object,
                                  passwordService.Object,
                                  mailService.Object,
                                  loggerService.Object);

            bool flag = service.ForgotPassword(null);
            Assert.IsFalse(flag);
        }
    }
}