namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.IO;

    /// <summary>
    /// File Service
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Logger Service
        /// </summary>
        private readonly ILoggerService _logger; 

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginService"/> class.
        /// </summary>
        public FileService(ILoggerService logger)
        {
            if (logger == null)
            {
                //if logger cannot be initialised then console.write
                Console.Write("ArgumentNullException: ILoggerService in FileService");
                throw new ArgumentNullException("ArgumentNullException: ILoggerService in FileService");
            }

            this._logger = logger; 
        }

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
                this._logger.Log(new Log(string.Format("Data stored at {0}",path), false));
                return true; 
            }
            catch(Exception e)
            {
                this._logger.Log(new Log(e.Message, true)); 
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
                this._logger.Log(new Log(string.Format("Data Retrieved from {0}", path), false));
                return picture; 
            }
            catch(Exception e)
            {
                this._logger.Log(new Log(e.Message, true)); 
                return null; 
            }
        }

        /// <summary>
        /// Deletes a file from the file system
        /// </summary>
        /// <param name="path">Path of the file to delete</param>
        /// <returns>Flag to indicate if operation was successful</returns>
        public bool DeleteMedia(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    this._logger.Log(new Log(string.Format("Data Deleted from {0}", path), false));
                    File.Delete(path);
                    return true; 
                }
            }
            catch(Exception e)
            {
                this._logger.Log(new Log(e.Message, true)); 
            }
            return false; 
        }
    }
}