namespace com.kiransprojects.travelme.DataAccess.Mappings
{
    using com.kiransprojects.travelme.Framework.Entities;
    using NHibernate.Mapping.ByCode.Conformist;
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
            this.Property(o => o.FirstName, p => { p.Length(100); });
            this.Property(o => o.LastName, p => { p.Length(100); });
            this.Property(o => o.DateOfBirth, p => { p.Type<DateTimeType>(); });
            this.Property(o => o.Email, p => { p.Length(500); });
            this.Property(o => o.UserPassword, p => { p.Length(128); });
            this.Property(
                o => o.ProfilePicture,
                p => 
                {
                    p.Type<BinaryBlobType>(); 
                    p.Length(Int32.MaxValue); 
                });

            this.ManyToOne(o => o.Trips,
                p =>
                {
                    p.Cascade(NHibernate.Mapping.ByCode.Cascade.All);
                    p.Class(typeof(Trip));
                    p.Column("UserID");
                });
        }
    }
}