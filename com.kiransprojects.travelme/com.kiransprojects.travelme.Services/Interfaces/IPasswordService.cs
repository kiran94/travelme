namespace com.kiransprojects.travelme.Services.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;

    /// <summary>
    /// Contract for Password Service
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Generates password, salt and stored appropiate fields
        /// </summary>
        /// <param name="User">New User to hash</param>
        /// <returns>User Entity with all credentials and fields</returns>
        UserEntity GenerateCredentials(UserEntity User);

        /// <summary>
        /// Generates a password from plaintext
        /// </summary>
        /// <param name="plaintext">string to convert</param>
        /// <returns>Hashed password</returns>
        string GeneratePassword(string plaintext, string salt);

        /// <summary>
        /// Generates a salt for the user
        /// </summary>
        /// <returns>generated salt</returns>
        string GenerateSalt();

        /// <summary>
        /// Verifies a user's password strength
        /// </summary>
        /// <param name="password">password to check</param>
        /// <returns></returns>
        bool VerifyPassword(string password);
    }
}