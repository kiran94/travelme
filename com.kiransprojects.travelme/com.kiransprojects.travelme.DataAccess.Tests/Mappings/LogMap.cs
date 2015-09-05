namespace com.kiransprojects.travelme.DataAccess.Mappings
{
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Log Map
    /// </summary>
    public class LogMap : ClassMapping<Log>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogMap"/> class.
        /// </summary>
        public LogMap()
        {
            this.Table("Logs");
            this.Id(o => o.ID);

            this.Property(o => o.LogMessage, p => { p.Length(255); });
            this.Property(o => o.LogDateTime, p => { p.Type<DateTimeType>(); });
            this.Property(o => o.Error, p => { p.Type<BooleanType>(); });
        }
    }
}