namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Service.Interfaces;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Post Service
    /// </summary>
    public class PostService : IPostService
    {
        /// <summary>
        /// Post Repository
        /// </summary>
        private readonly IRepository<Post> _repository = null;

        /// <summary>
        /// Logger Repository
        /// </summary>
        private readonly ILoggerService _logger = null;

        /// <summary>
        /// Media Service
        /// </summary>
        private readonly IMediaService _mediaService = null;

        /// <summary>
        /// File Service
        /// </summary>
        private readonly IFileService _fileService = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        public PostService(
            IRepository<Post> postRepository, 
            ILoggerService loggerService,
            IMediaService mediaService, 
            IFileService fileService)
        {
            if(postRepository == null)
            {
                throw new ArgumentNullException("Post Repository"); 
            }

            if (loggerService == null)
            {
                throw new ArgumentNullException("Logger Service");
            }

            if(mediaService == null)
            {
                throw new ArgumentNullException("Media Service");
            }

            if (fileService == null)
            {
                throw new ArgumentNullException("File Service");
            }

            this._repository = postRepository; 
            this._logger = loggerService; 
            this._mediaService = mediaService;
            this._fileService = fileService; 
        }

        /// <summary>
        /// Gets List of all Posts
        /// </summary>
        /// <returns>List of posts</returns>
        public IList<Post> GetPosts()
        {
            return this._repository.GetAll(); 
        }

        /// <summary>
        /// Gets a post by its ID
        /// </summary>
        /// <param name="ID">Post ID</param>
        /// <returns>Post Object</returns>
        public Post GetPost(Guid ID)
        {
            return this._repository.GetByID(ID);
        }

        /// <summary>
        /// Adds a post
        /// </summary>
        /// <param name="post">post to insert</param>
        /// <returns>flag indicating if insert was successful</returns>
        public bool AddPost(Post post)
        {
            if(post == null)
            {
                return false; 
            }

            try
            {
                this._repository.Insert(post);
                return true; 
            }
            catch(Exception e)
            {
                Log log = new Log(e.Message, true);
                this._logger.Log(log);
                return false; 
            }
        }

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post">post to update with updated data</param>
        /// <returns>flag indicating if update was successfull</returns>
        public bool EditPost(Post post)
        {
            if(post == null)
            {
                return false; 
            }

            try
            {
                this._repository.Update(post, true);
                return true; 
            }
            catch(Exception e)
            {
                Log log = new Log(e.Message, true);
                this._logger.Log(log);
                return false; 
            }
        }

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="post">post to delete</param>
        /// <returns>flag indicating if delete was successfull</returns>
        public bool DeletePost(Post post)
        {
            if(post == null)
            {
                return false; 
            }

            try
            {
                this._repository.Delete(post);
                return true; 
            }
            catch(Exception e)
            {
                Log log = new Log(e.Message, true);
                this._logger.Log(log);
                return false; 
            }
        }

        /// <summary>
        /// Adds a photo to the post
        /// </summary>
        /// <param name="postID">Post ID to add media too</param>
        /// <param name="photo">Photo to add</param>
        /// <returns>Media object created</returns>
        public Media AddPhoto(Guid postID, string path, byte[] photo)
        {
            if(photo == null)
            {
                return null; 
            }

            Media media = new Media();
            media.ID = Guid.NewGuid();
            media.MediaData = string.Format("{0}/{1}.jpg", path, media.ID);
            media.RelationID = postID; 

            if(this._fileService.SaveMedia(media.MediaData, photo))
            {
                this._mediaService.StoreMedia(media); 
                return media; 
            }

            return null; 
        }
    }
}