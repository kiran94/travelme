namespace com.kiransprojects.travelme.DataAccess.Interfaces
{
    using NHibernate.Cfg; 

    /// <summary>
    /// Contract for Nhibernate configuration
    /// </summary>
    public interface IDatabaseConfig
    {
        /// <summary>
        /// Gets the configuration for nhibernate
        /// </summary>
        /// <returns></returns>
        Configuration GetConfig(); 

    }
}