namespace com.kiransprojects.travelme.DataAccess.Interfaces
{
    using NHibernate;

    /// <summary>
    /// Contract for Nhibernate Helper
    /// </summary>
    public interface INhibernateHelper
    {
        ISession GetSession();
    }
}