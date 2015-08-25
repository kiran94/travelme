namespace com.kiransprojects.travelme.DataAccess.Interfaces
{
    using NHibernate;

    /// <summary>
    /// Contract for Nhibernate Helper
    /// </summary>
    public interface INhibernateHelper
    {
        /// <summary>
        /// Gets session for use in database transactions
        /// </summary>
        /// <returns>ISession implementation</returns>
        ISession GetSession();
    }
}
