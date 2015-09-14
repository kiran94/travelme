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
            string plaintext = "Password123456789"; 

            UserEntity user = new UserEntity();
            user.ID = Guid.NewGuid();
            user.FirstName = "Test";
            user.UserPassword = "Password123456789"; 

            PasswordService service = new PasswordService();
            UserEntity retrieved = service.GenerateCredentials(user);

            StringAssert.DoesNotMatch(plaintext, retrieved.UserPassword);
            Assert.NotNull(user.Registered);
            Assert.NotNull(user.Salt);
            Assert.NotNull(user.UserPassword);
        }

        /// <summary>
        /// Ensures null is returned when user entity is null 
        /// </summary>
        [Test]
        public void GenerateCredentials_NullUserEntity_ReturnsNull()
        {
            PasswordService service = new PasswordService();
            UserEntity retrieved = service.GenerateCredentials(null);
            Assert.IsNull(retrieved);
        }

        /// <summary>
        /// Ensures null is returned when the password is empty
        /// </summary>
        [Test]
        public void GenerateCredentials_EmptyPassword_ReturnsNull()
        {
            UserEntity user = new UserEntity();
            user.ID = Guid.NewGuid();
            user.FirstName = "Test";
            user.UserPassword = string.Empty;

            PasswordService service = new PasswordService();
            UserEntity retrieved = service.GenerateCredentials(user);
            Assert.IsNull(retrieved);
        }

        /// <summary>
        /// Ensures the function returns a string when given valid strings
        /// </summary>
        [Test]
        public void GeneratePassword_ValidString_ReturnsString()
        {
            PasswordService service = new PasswordService();
            string password = service.GeneratePassword("TestingTest56734500", "1LavZgCrOZg2+YPs5EX59NNwP+ZwxfUT6u6dZ3ZkeZB049KHVyTWC8bZ2S2Gp9FYcmxDHZwojXDbyIHehR5z/9P8bVgL6xf6w9G/ybwPosj/ibEKc4iAq7MNiEWjbAB7cAjeTJ+Eeu7KGSenAOFAW9dwS0aWtzmLHxoCG7XgdtE=");
            Assert.NotNull(password);
        }

        /// <summary>
        /// Ensures an empty string is returned when empty salt is passed
        /// </summary>
        [Test]
        public void GeneratePassword_EmptySalt_ReturnsEmpty()
        {
            PasswordService service = new PasswordService();
            string password = service.GeneratePassword("TestingTest56734500", string.Empty);
            Assert.AreEqual(string.Empty, password);
        }

        /// <summary>
        /// Ensures an empty string is returned when an empty password is passed
        /// </summary>
        [Test]
        public void GeneratePassword_EmptyPassword_ReturnsEmpty()
        {
            PasswordService service = new PasswordService();
            string password = service.GeneratePassword(string.Empty, "1LavZgCrOZg2+YPs5EX59NNwP+ZwxfUT6u6dZ3ZkeZB049KHVyTWC8bZ2S2Gp9FYcmxDHZwojXDbyIHehR5z/9P8bVgL6xf6w9G/ybwPosj/ibEKc4iAq7MNiEWjbAB7cAjeTJ+Eeu7KGSenAOFAW9dwS0aWtzmLHxoCG7XgdtE=");
            Assert.NotNull(password);
        }

        /// <summary>
        /// Ensures salt is returned
        /// </summary>
        [Test]
        public void GenerateSalt_NoParam_ReturnsSalt()
        {
            PasswordService service = new PasswordService();
            string salt = service.GenerateSalt();
            Assert.NotNull(salt);
            Assert.That(salt.Length >= 128);
        }

        /// <summary>
        /// Ensures true is returned for a password with 1+ upper, 1+ lower and 1+ number with a min length of 8
        /// </summary>
        [Test]
        public void VerifyPassword_StrongPassword_ReturnTrue()
        {
            PasswordService service = new PasswordService();
            bool flag = service.VerifyPassword("JsdffdsD342234");
            Assert.IsTrue(flag);
        }

        /// <summary>
        /// Ensures false is returned when no upper case characters are present
        /// </summary>
        [Test]
        public void VerifyPassword_NoUpperCase_ReturnFalse()
        {
            PasswordService service = new PasswordService();
            bool flag = service.VerifyPassword("dsfgdsfljdsg456");
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures false is returned when no lower case characters are present
        /// </summary>
        [Test]
        public void VerifyPassword_NoLowerCase_ReturnFalse()
        {
            PasswordService service = new PasswordService();
            bool flag = service.VerifyPassword("IOJDSFDS23F");
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures false is returned when only 4 characters are present
        /// </summary>
        [Test]
        public void VerifyPassword_Length4_ReturnFalse()
        {
            PasswordService service = new PasswordService();
            bool flag = service.VerifyPassword("Te43");
            Assert.IsFalse(flag);
        }

        /// <summary>
        /// Ensures false is returned when 105 characters are present
        /// </summary>
        [Test]
        public void VerifyPassword_Length105_ReturnFalse()
        {
            PasswordService service = new PasswordService();
            bool flag = service.VerifyPassword("TESTestTESTestTESTestTESTestTESTestTESTestTESTESTestTestTESTestTESTestTESTestTESTestTETESTestTESTestSTe23");
            Assert.IsFalse(flag);
        }
    }
}