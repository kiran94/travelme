namespace com.kiransprojects.travelme.DataAccess.Mappings
{
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// Mapping for media entity
    /// </summary>
    public class MediaMap : ClassMapping<Media>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaMap"/> class.
        /// </summary>
        public MediaMap()
        {
            this.Table("Media");
            this.Id(o => o.ID);
            this.Property(o => o.MediaData, p => { p.Length(1000); });
        }
    }
}