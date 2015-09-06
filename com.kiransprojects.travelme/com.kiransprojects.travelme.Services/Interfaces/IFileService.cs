namespace com.kiransprojects.travelme.Services.Interfaces
{
    /// <summary>
    /// Contract for file service operations
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Saves media to the path
        /// </summary>
        /// <param name="path">Path to save too</param>
        /// <param name="picture">Picture to save</param>
        /// <returns>FLag indicating if operation was successfull</returns>
        bool SaveMedia(string path, byte[] picture);

        /// <summary>
        /// Gets method from the path
        /// </summary>
        /// <param name="path">Path to return media from</param>
        /// <returns>Media</returns>
        byte[] GetMedia(string path); 
    }
}