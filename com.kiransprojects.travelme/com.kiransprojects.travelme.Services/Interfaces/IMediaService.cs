namespace com.kiransprojects.travelme.Services.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Constract for Media Service
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Gets Media
        /// </summary>
        /// <param name="id">ID of media file</param>
        /// <returns></returns>
        Media GetMedia(Guid id);

        /// <summary>
        /// Stored Media
        /// </summary>
        /// <param name="media">Media to store</param>
        void StoreMedia(Media media);

        /// <summary>
        /// Gets Filtered Media
        /// </summary>
        /// <param name="filter">Expression of media to filter</param>
        /// <returns></returns>
        IList<Media> GetFilteredMedia(Func<Media, Media> filter);
    }
}