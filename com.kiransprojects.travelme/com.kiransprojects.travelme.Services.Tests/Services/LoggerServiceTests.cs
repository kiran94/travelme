﻿namespace com.kiransprojects.travelme.Services.Tests.Services
{
    using com.kiransprojects.travelme.DataAccess.Interfaces;
    using com.kiransprojects.travelme.Framework.Entities;
    using com.kiransprojects.travelme.Services.Services;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Logger Service
    /// </summary>
    [TestFixture]
    public class LoggerServiceTests
    {
        /// <summary>
        /// Ensures logs can be persisted to the repository layer
        /// </summary>
        [Test]
        public void Log_ValidLog_ReturnTrue()
        {
            Mock<IRepository<Log>> Repository = new Mock<IRepository<Log>>();
            Repository.Setup(o => o.Insert(It.IsAny<Log>())); 

            Log log = new Log()
            {
                ID = Guid.NewGuid()
            }; 

            LoggerService Service = new LoggerService(Repository.Object);
            bool flag = Service.Log(log);
            Assert.IsTrue(flag); 
        }

        /// <summary>
        /// Ensures a null log object returns false
        /// </summary>
        [Test]
        public void Log_NullLog_ReturnFalse()
        {
            Mock<IRepository<Log>> Repository = new Mock<IRepository<Log>>();
            
            LoggerService Service = new LoggerService(Repository.Object);
            bool flag = Service.Log(null);
            Assert.IsFalse(flag);         
        }

        /// <summary>
        /// Ensures All logs are retrieved when they exist
        /// </summary>
        [Test]
        public void GetLogs_LogsExist_ReturnsList()
        {
            IList<Log> logs = new List<Log>()
            {
                new Log(),
                new Log(),
                new Log(),
                new Log() 
            };

            Mock<IRepository<Log>> Repository = new Mock<IRepository<Log>>();
            Repository.Setup(o => o.GetAll()).Returns(logs);

            LoggerService Service = new LoggerService(Repository.Object);
            IList<Log> RetrievedLogs = Service.GetLogs();
            CollectionAssert.AreEqual(logs, RetrievedLogs); 
        }

        /// <summary>
        /// Ensures when logs dont exist, null list is returned
        /// </summary>
        [Test]
        public void GetLogs_LogsNotExist_ReturnsNullList()
        {
            Mock<IRepository<Log>> repository = new Mock<IRepository<Log>>();
            repository.SetupSequence(o => o.GetAll()).Returns(null);

            LoggerService service = new LoggerService(repository.Object);
            IList<Log> logs = service.GetLogs();
            Assert.IsNull(logs);
        }
        
        /// <summary>
        /// Ensures when a single log exist it is retrieved
        /// </summary>
        [Test]
        public void GetLog_LogExist_LogReturned()
        {
            Log log = new Log()
            {
                ID = Guid.NewGuid()
            };
            Mock<IRepository<Log>> repository = new Mock<IRepository<Log>>();
            repository.SetupSequence(o => o.GetByID(It.IsAny<Guid>())).Returns(log);

            LoggerService service = new LoggerService(repository.Object);

            Assert.AreEqual(log, service.GetLog(Guid.NewGuid()));
        }
    }
}