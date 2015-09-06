namespace com.kiransprojects.travelme.Service
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
        /// Initializes a new instance of the <see cref="RepositoryBaseUserService"/> class.
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
        /// </summary>
        /// <param name="ID">ID of the user to add</param>
        /// <param name="picture">Actual picture to save</param>
        /// <returns>path of profile picture</returns>
        public string AddProfilePicture(Guid ID, byte[] picture)
        {
            if(ID == null || picture == null)
            {
                return string.Empty; 
            }

            UserEntity Entity = this._repository.GetByID(ID); 
            
            if(Entity == null)
            {
                return string.Empty; 
            }

            string path = string.Format("Profile{0}.jpg", ID.ToString());
            if(this._fileservice.SaveMedia(path, picture))
            {
                return path; 
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removed a profile picture
        /// </summary>
        /// <param name="ID">User to remove picture from</param>
        /// <returns></returns>
        public bool RemoveProfilePicture(Guid ID)
        {
            throw new NotImplementedException();
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