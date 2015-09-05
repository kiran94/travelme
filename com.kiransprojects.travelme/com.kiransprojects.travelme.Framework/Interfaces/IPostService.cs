namespace com.kiransprojects.travelme.Framework.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contract for Post Services
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Gets all the posts for a specific trip
        /// </summary>
        /// <param name="ID">Trip ID</param>
        /// <returns>List of Posts</returns>
        IList<Post> GetPosts(Guid ID);

        /// <summary>
        /// Adds a post to the database
        /// </summary>
        /// <param name="post">Post to add</param>
        /// <returns>Flag to indicate if post was saved</returns>
        bool AddPost(Post post);

        /// <summary>
        /// Edits an existing post
        /// </summary>
        /// <param name="post">Post to edit</param>
        /// <returns>Flag to indicate if post was edited</returns>
        bool EditPost(Post post);

        /// <summary>
        /// Deletes an post
        /// </summary>
        /// <param name="post">Post to delete</param>
        /// <returns>Flag that indicates if a post was deleted</returns>
        bool DeletePost(Post post);

        /// <summary>
        /// Adds a photo to a post
        /// </summary>
        /// <param name="photo">Photo to add</param>
        /// <returns></returns>
        Media AddPhoto(byte[] photo);
    }
}