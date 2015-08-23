namespace com.kiransprojects.travelme.DataAccess.Tests.Mappings
{
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// Mapping for Post Entity
    /// </summary>
    public class PostMap : ClassMapping<Post>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostMap"/> class.
        /// </summary>
        public PostMap()
        {
            this.Table("Post");
            this.Id(o => o.ID);
            this.Property(o => o.PostData, p => { p.Length(256); p.NotNullable(true); });
            this.Property(o => o.PostLat, p => { p.Length(11); });
            this.Property(o => o.PostLong, p => { p.Length(11); });

            this.Bag(
                o => o.Media,
                p =>
                {
                    p.Table("Media");
                    p.Cascade(Cascade.None);
                    p.Lazy(CollectionLazy.NoLazy);
                    p.Key(
                        k =>
                        {
                            k.Column("PostID");
                        });
                }, 
                map => map.OneToMany(p => p.Class(typeof(Media))));
        }
    }
}