namespace com.kiransprojects.travelme.DataAccess.Mappings
{
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate.Mapping.ByCode; 
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// Mapping class for Trip Entity
    /// </summary>
    public class TripMap : ClassMapping<Trip>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TripMap"/> class.
        /// </summary>
        public TripMap()
        {
            this.Table("Trip");
            this.Id(o => o.ID);
            this.Property(o => o.TripName, p => { p.Length(20); p.NotNullable(true); });
            this.Property(o => o.TripDescription, p => { p.Length(50); });
            this.Property(o => o.TripLocation, p => { p.Length(75); });

            this.Property(o => o.RelationID,
                p =>
                {
                    p.Column("UserID");
                    p.NotNullable(true);
                }); 

            this.Bag(
              o => o.Posts,
              p =>
              {
                  p.Table("Post");
                  p.Cascade(Cascade.All);
                  p.Inverse(true);
                  p.Fetch(CollectionFetchMode.Select);
                  p.Lazy(CollectionLazy.NoLazy);
                  p.Inverse(false); 
                  p.Key(
                      k =>
                      {
                          k.Column("TripID");
                      });
              },
              map => map.OneToMany(p => p.Class(typeof(Post))));
        }
    }
}