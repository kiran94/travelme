namespace com.kiransprojects.travelme.Services.Interfaces
{
    using com.kiransprojects.travelme.Framework.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Constract for Logger Service
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Make a log
        /// </summary>
        /// <param name="log">Log object to log</param>
        bool Log(Log log);

        /// <summary>
        /// Get all logs
        /// </summary>
        /// <returns>List of Log Objects</returns>
        IList<Log> GetLogs();

        /// <summary>
        /// Get a log by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Log GetLog(Guid ID); 
    }
}