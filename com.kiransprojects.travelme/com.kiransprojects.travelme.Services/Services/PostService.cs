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


        private readonly ILoggerService _logger = null; 

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        public PostService(IRepository<Post> postRepository, ILoggerService loggerService)
        {
            if(postRepository == null)
            {
                throw new ArgumentNullException("Post Repository"); 
            }

            if (loggerService == null)
            {
                throw new ArgumentNullException("Logger Service");
            }

            this._logger = loggerService; 
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
                return false; 
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="post">post to delete</param>
        /// <returns>flag indicating if delete was successfull</returns>
        public bool DeletePost(Post post)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a photo to the post
        /// </summary>
        /// <param name="postID">Post ID to add media too</param>
        /// <param name="photo">Photo to add</param>
        /// <returns>Media object created</returns>
        public Media AddPhoto(Guid postID, byte[] photo)
        {
            throw new NotImplementedException();
        }
    }
}