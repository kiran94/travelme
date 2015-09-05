namespace com.kiransprojects.travelme.Framework.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;

    /// <summary>
    /// Contract for User Services
    /// </summary>
    public interface IUserService
    {
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
        Media AddProfilePicture(Guid ID, byte[] picture);

        /// <summary>
        /// Edits a profile picture to a user
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <param name="picture">Picture to update with</param>
        /// <returns></returns>
        Media EditProfilePicture(Guid ID, byte[] picture);

        /// <summary>
        /// Removes a profile picture for a user
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        bool RemoveProfilePicture(Guid ID); 
    }
}