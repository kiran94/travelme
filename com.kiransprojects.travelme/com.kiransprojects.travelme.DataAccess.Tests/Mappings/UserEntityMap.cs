namespace com.kiransprojects.travelme.DataAccess.Mappings
{
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Mapping.ByCode; 
    using NHibernate.Type;
    using System;

    /// <summary>
    /// Maps User Entity object to database table
    /// </summary>
    public class UserEntityMap : ClassMapping<UserEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEntityMap"/> class.
        /// </summary>
        public UserEntityMap()
        {
            this.Table("UserEntity");
            this.Id(o => o.ID);
            this.Property(o => o.FirstName, p => { p.Length(100); p.NotNullable(true); });
            this.Property(o => o.LastName, p => { p.Length(100); });
            this.Property(o => o.DateOfBirth, p => { p.Type<DateTimeType>(); });
            this.Property(o => o.Email, p => { p.Length(500); p.NotNullable(true); });
            this.Property(o => o.UserPassword, p => { p.Length(128); p.NotNullable(true); });
            this.Property(o => o.ProfilePicture, p => { p.Length(1000); });           

            this.Bag(
                o => o.Trips,
                p =>
                {
                    p.Table("Trip");
                    p.Cascade(Cascade.All);
                    p.Inverse(true);
                    p.Fetch(CollectionFetchMode.Select);
                    p.Lazy(CollectionLazy.NoLazy);
                    p.Inverse(false); 
                    p.Key(
                        k =>
                        {
                            k.Column("UserID");
                        });
                },
                map => map.OneToMany(p => p.Class(typeof(Trip))));
        }
    }
}