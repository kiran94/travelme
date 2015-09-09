﻿namespace com.kiransprojects.travelme.Service
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Service.Interfaces;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// User Service
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Repository for User Entity
        /// </summary>
        private readonly IRepository<UserEntity> _repository = null;

        /// <summary>
        /// File Service
        /// </summary>
        private readonly IFileService _fileservice = null; 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService(
            IRepository<UserEntity> Repository, 
            IFileService FileService)
        {
            if(Repository == null)
            {
                throw new NullReferenceException("User Service Repository Null"); 
            }

            if(FileService == null)
            {
                throw new NullReferenceException("File Service Null"); 
            }

            this._repository = Repository;
            this._fileservice = FileService; 
        }

        /// <summary>
        /// Gets a user through an ID
        /// </summary>
        /// <param name="ID">User ID</param>
        /// <returns>User Entity</returns>
        public UserEntity GetUser(Guid ID)
        {
            UserEntity ToReturn = null; 
            try
            {
                ToReturn = this._repository.GetByID(ID);
                
            }
            catch(Exception e)
            {
                //Log log = new Log(e, true); 
                Debugger.Log(0, "Error", e.Message);
            }
            return ToReturn; 
        }

        /// <summary>
        /// Adds a profile picture of the user to the system. 
        /// Guid and Byte are not nullable data types so no checks needed
        /// </summary>
        /// <param name="ID">ID of the user to add</param>
        /// <param name="picture">Actual picture to save</param>
        /// <returns>path of profile picture</returns>
        public string AddProfilePicture(Guid ID, string path, byte[] picture)
        {
            UserEntity Entity = this._repository.GetByID(ID); 
            
            if(Entity == null)
            {
                return string.Empty; 
            }

            string fullpath = string.Format("{0}/Profile{1}.jpg", path ,ID.ToString());
            if(this._fileservice.SaveMedia(path, picture))
            {
                Entity.ProfilePicture = path;
                this._repository.Update(Entity, false);
                return fullpath; 
            }

            return string.Empty; 
        }

        /// <summary>
        /// Edits a profile picture of the user
        /// </summary>
        /// <param name="ID">ID of the user</param>
        /// <param name="picture">Actual picture to save</param>
        /// <returns></returns>
        public bool EditProfilePicture(Guid ID, byte[] picture)
        {
            UserEntity Entity = this._repository.GetByID(ID);

            if (Entity == null || string.IsNullOrEmpty(Entity.ProfilePicture))
            {
                return false;
            }

            this._fileservice.SaveMedia(Entity.ProfilePicture, picture);
            return true; 
        }

        /// <summary>
        /// Removed a profile picture
        /// </summary>
        /// <param name="ID">User to remove picture from</param>
        /// <returns>Flag indicating if operation was successfull</returns>
        public bool RemoveProfilePicture(Guid ID)
        {
            UserEntity Entity = this._repository.GetByID(ID);

            if(Entity == null || string.IsNullOrEmpty(Entity.ProfilePicture))
            {
                return false; 
            }

            bool flag = this._fileservice.DeleteMedia(Entity.ProfilePicture);

            return flag; 
        }

        /// <summary>
        /// Flag indicates if the repository is set or not
        /// </summary>
        /// <returns></returns>
        public bool isRepositorySet()
        {
            return (this._repository != null) ? true : false; 
        }
    }
}