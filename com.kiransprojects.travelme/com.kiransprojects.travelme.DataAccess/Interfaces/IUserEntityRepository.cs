using com.kiransprojects.travelme.Framework.Entities;
namespace com.kiransprojects.travelme.DataAccess.Interfaces
{
    /// <summary>
    /// Contract for the UserEntity Repository
    /// </summary>
    public interface IUserEntityRepository : IRepository<UserEntity>
    {
        /// <summary>
        /// Authenticates if a user's credentials are correct
        /// </summary>
        /// <param name="Email">User's Email</param>
        /// <param name="password">User's Password</param>
        /// <param name="Role">User's Role to be set</param>
        /// <returns>Flag indicating if the user has been authenticated</returns>
        bool Authenticate(string Email, string password, out string Role);
    }
}