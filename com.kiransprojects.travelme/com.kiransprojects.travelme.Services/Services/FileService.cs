namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.IO;

    /// <summary>
    /// File Service
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Saves media to the path
        /// </summary>
        /// <param name="path">Path to save too</param>
        /// <param name="picture">Picture to save</param>
        /// <returns>FLag indicating if operation was successfull</returns>
        public bool SaveMedia(string path, byte[] picture)
        {
            StreamWriter Writer; 
            try
            {
                Writer = new StreamWriter(path);
                Writer.WriteLine(picture.ToString());
                Writer.Close();
                return true; 
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message); 
                return false; 
            }
        }

        /// <summary>
        /// Gets method from the path
        /// </summary>
        /// <param name="path">Path to return media from</param>
        /// <returns>Media</returns>
        public byte[] GetMedia(string path)
        {
            StreamReader Reader;
            try
            {
                Reader = new StreamReader(path);
                string string_picture = Reader.ReadToEnd(); 
                Reader.Close();

                byte[] picture = System.Text.Encoding.UTF8.GetBytes(string_picture);
                return picture; 
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                return null; 
            }
        }
    }
}