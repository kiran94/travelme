
namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Services;
    using NUnit.Framework;
    using System;

    /// <summary>
    /// PasswordService Tests
    /// </summary>
    [TestFixture]
    public class PasswordServiceTests
    {
        /// <summary>
        /// Ensures credentials can be generated for a user
        /// </summary>
        [Test]
        public void GenerateCredentials_ValidUserEntity_ReturnedWithFields()
        {
            UserEntity user = new UserEntity();
            user.ID = Guid.NewGuid();
            user.FirstName = "Test";
            user.UserPassword = "Password123456789"; 

            PasswordService service = new PasswordService();
            UserEntity Retrieved = service.GenerateCredentials(user);

            StringAssert.DoesNotMatch(user.UserPassword, Retrieved.UserPassword);
            Assert.NotNull(user.Registered);
        }

        [Test]
        public void GenerateCredentials_NullUserEntity_ReturnsNull()
        {
            throw new NotImplementedException(); 
        }

        [Test]
        public void GenerateCredentials_EmptyEmail_ReturnsNull()
        {
            throw new NotImplementedException(); 
        }

        [Test]
        public void GenerateCredentials_EmptyPassword_ReturnsNull()
        {
            throw new NotImplementedException(); 
        }

        [Test]
        public void GeneratePassword_ValidString_ReturnsPassword()
        {
            throw new NotImplementedException(); 
        }

        [Test]
        public void GeneratePassword_EmptyString_ReturnsEmptyString()
        {
            throw new NotImplementedException(); 
        }

        [Test]
        public void GeneratePassword_EmptyNull_ReturnsEmptyString()
        {
            throw new NotImplementedException(); 
        }


    }
}