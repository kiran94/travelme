namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Service.Interfaces;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;

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
        /// Logger Service
        /// </summary>
        private readonly ILoggerService _loggerservice = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="Repository">User Repository</param>
        /// <param name="FileService">File Service</param>
        /// <param name="LoggerService">Logger Service</param>
        public UserService(
            IUserEntityRepository Repository,
            IFileService FileService,
            ILoggerService LoggerService)
        {
            if (Repository == null)
            {
                throw new NullReferenceException("User Service Repository Null");
            }

            if (FileService == null)
            {
                throw new NullReferenceException("File Service Null");
            }

            if (LoggerService == null)
            {
                throw new NullReferenceException("Logger Service Null");
            }

            this._repository = Repository;
            this._fileservice = FileService;
            this._loggerservice = LoggerService;
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
            catch (Exception e)
            {
                Log log = new Log(e.Message, true);
                _loggerservice.Log(log);
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

            if (Entity == null)
            {
                return string.Empty;
            }

            string fullpath = string.Format("{0}/Profile{1}.jpg", path, ID.ToString());
            if (this._fileservice.SaveMedia(path, picture))
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

            if (Entity == null || string.IsNullOrEmpty(Entity.ProfilePicture))
            {
                return false;
            }

            bool flag = this._fileservice.DeleteMedia(Entity.ProfilePicture);

            return flag;
        }

        /// <summary>
        /// Flag indicates if the repository is set or not
        /// </summary>
        /// <returns>Flag indicating if the repository is set</returns>
        public bool isRepositorySet()
        {
            return (this._repository != null) ? true : false;
        }

        /// <summary>
        /// Updates the given user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>Flag to indicate if successfull</returns>
        public bool UpdateUser(UserEntity user)
        {
            try
            {
                this._repository.Update(user, false);
                return true; 
            }
            catch (Exception e)
            {
                this._loggerservice.Log(new Log(e.Message, true));
                return false;
            }
        }
    }
}