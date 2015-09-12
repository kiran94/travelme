namespace com.kiransprojects.travelme.Services.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Logger Service
    /// </summary>
    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// Log Repository
        /// </summary>
        private readonly IRepository<Log> _repository = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerService"/> class.
        /// </summary>
        /// <param name="repository">Repository to CRUD log objects</param>
        public LoggerService(IRepository<Log> repository)
        {
            if(repository == null)
            {
                throw new ArgumentNullException("Logger Repository null"); 
            }

            this._repository = repository; 
        }
        
        /// <summary>
        /// Save the log to the database
        /// </summary>
        /// <param name="log">Log object to log</param>
        public bool Log(Log log)
        {
            if (log == null) return false; 

            try
            {
                this._repository.Insert(log);
                return true; 
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                return false; 
            }
        }

        /// <summary>
        /// Gets all Logs
        /// </summary>
        /// <returns>List of all logs in the repository</returns>
        public IList<Log> GetLogs()
        {
            return this._repository.GetAll(); 
        }

        /// <summary>
        /// Gets a log by its ID
        /// </summary>
        /// <param name="ID">Log ID</param>
        /// <returns>Log Object</returns>
        public Log GetLog(Guid ID)
        {
            return this._repository.GetByID(ID); 
        }
    }
}