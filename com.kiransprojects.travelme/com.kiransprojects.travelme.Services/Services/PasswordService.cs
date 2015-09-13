namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Password Service
    /// </summary>
    public class PasswordService : IPasswordService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordService"/> class.
        /// </summary>
        public PasswordService()
        {
            
        }

        /// <summary>
        /// Generates password, salt and stored appropiate fields
        /// </summary>
        /// <param name="User">New User to hash</param>
        /// <returns>User Entity with all credentials and fields</returns>
        public UserEntity GenerateCredentials(UserEntity User)
        {
            string plaintext = User.UserPassword; 
            string salt = string.Empty; 

            if(string.IsNullOrEmpty(plaintext))
            {
                return null; 
            }

            salt = this.GenerateSalt(); 

            User.UserPassword = this.GeneratePassword(plaintext, salt);
            User.Salt = salt; 
            User.Registered = DateTime.Now;
            User.InvalidPassswordCount = 0;

            return User; 
        }

        /// <summary>
        /// Generates a password from plaintext
        /// </summary>
        /// <param name="plaintext">string to convert</param>
        /// <returns>Hashed password</returns>
        public string GeneratePassword(string plaintext, string salt)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            StringBuilder builder = new StringBuilder();
            builder.Append(plaintext).Append(salt);

            Byte[] inputBytes = Encoding.UTF8.GetBytes(builder.ToString());
            Byte[] hashedBytes = provider.ComputeHash(inputBytes);

            return Convert.ToBase64String(hashedBytes);
        }

        /// <summary>
        /// Generates a salt for the user
        /// </summary>
        /// <returns>generated salt</returns>
        public string GenerateSalt()
        {
            const short SALT_SIZE = 128;
            byte[] bytes = new byte[SALT_SIZE];

            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider(); 
            provider.GetBytes(bytes);
            
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Verifies a user's password strength
        /// </summary>
        /// <param name="password">password to check</param>
        /// <returns></returns>
        public bool VerifyPassword(string password)
        {
            throw new NotImplementedException();
        }        
    }
}