namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Media Service
    /// </summary>
    public class MediaService : IMediaService
    {
        /// <summary>
        /// Media Repository
        /// </summary>
        private readonly IRepository<Media> _repository;

        /// <summary>
        /// Logger Service
        /// </summary>
        private readonly ILoggerService _loggerService; 

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaService"/> class.
        /// </summary>
        public MediaService(IRepository<Media> repository, ILoggerService loggerService)
        {
            if(repository == null)
            {
                throw new ArgumentNullException("Media Repository"); 
            }

            if(loggerService == null)
            {
                throw new ArgumentNullException("Logger Service"); 
            }

            this._repository = repository;
            this._loggerService = loggerService; 
        }

        /// <summary>
        /// Gets Media by ID
        /// </summary>
        /// <param name="id">ID of the media to get</param>
        /// <returns>Media Object</returns>
        public Media GetMedia(Guid ID)
        {
            return this._repository.GetByID(ID); 
        }

        /// <summary>
        /// Stored Media
        /// </summary>
        /// <param name="media">Stored media record</param>
        public bool StoreMedia(Media media)
        {
            if(media == null)
            {
                return false; 
            }

            try
            {
                this._repository.Insert(media);
                return true; 
            }
            catch(Exception e)
            {
                Log log = new Log(e.Message, true);
                this._loggerService.Log(log);
                return false; 
            }
        }

        /// <summary>
        /// Gets filtered media through a lambda function
        /// </summary>
        /// <param name="filter"> parameterized delegate</param>
        /// <returns>List of Media Objects</returns>
        public IList<Media> GetFilteredMedia(Func<Media, Media> filter)
        {
            throw new NotImplementedException();
        }
    }
}