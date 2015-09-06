namespace com.kiransprojects.travelme.Service.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;

    /// <summary>
    /// Contract for Login Services
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Registers the passed user
        /// </summary>
        /// <param name="User">User to register, pass by reference so that profile picture path can be added</param>
        /// <returns>Flag indicates whether user was registered or not</returns>
        bool RegisterUser(out UserEntity User);

        /// <summary>
        /// Signs a User in
        /// </summary>
        /// <param name="Email">Email the user is registered under</param>
        /// <param name="Password">Password the user is registered with</param>
        /// <returns></returns>
        bool SignIn(string Email, string Password);
    }
}