namespace com.kiransprojects.travelme.Services.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;

    /// <summary>
    /// Contract for User Services
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Adds a user 
        /// </summary>
        /// <param name="user">User to add</param>
        /// <returns>Flag indicating if the user is added</returns>
        bool AddUser(UserEntity user);

        /// <summary>
        /// Gets a User Entity 
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <returns>User Entity Object</returns>
        UserEntity GetUser(Guid ID);

        /// <summary>
        /// Adds a profile picture to a user
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <param name="picture">Picture to save</param>
        /// <returns></returns>
        string AddProfilePicture(Guid ID, string path, byte[] picture);

        /// <summary>
        /// Edits a profile picture to a user
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <param name="picture">Picture to update with</param>
        /// <returns></returns>
        bool EditProfilePicture(Guid ID, byte[] picture);

        /// <summary>
        /// Removes a profile picture for a user
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        bool RemoveProfilePicture(Guid ID); 

        /// <summary>
        /// Updates the given user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>Flag to indicate if successfull</returns>
        bool UpdateUser(UserEntity user);
    }
}