namespace com.kiransprojects.travelme.Framework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a User Entity
    /// </summary>
    public class UserEntity : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEntity"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Properties required to be virtual for Nhibernate")]
        public UserEntity()
        {
            this.Trips = new List<Trip>(); 
        }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        [Required]
        [Display(Name="First Name")]
        [StringLength(100, ErrorMessage="{0} has a maximum length of {1} characters")]
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        [Display(Name="Last Name")]
        [StringLength(100, ErrorMessage = "{0} has a maximum length of {1} characters")]
        [Required]
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth
        /// </summary>
        [Display(Name="Date Of Birth")]
        [DataType(DataType.DateTime, ErrorMessage="Please enter a valid date")]
        [DisplayFormat(NullDisplayText="-")]
        public virtual DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        [Required]
        [Display(Name="Email")]
        [StringLength(500, ErrorMessage = "{0} has a maximum length of {1} characters")]
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the user passwords
        /// </summary>
        [Required]
        [Display(Name="Password")]
        [StringLength(256, ErrorMessage = "{0} has a maximum length of {1} characters")]
        public virtual string UserPassword { get; set; }

        /// <summary>
        /// Gets or sets the profile picture
        /// </summary>
        [Display(Name="Profile Picture")]
        [StringLength(1000, ErrorMessage="{0} has a maximum length of {1} characters")]
        public virtual string ProfilePicture { get; set; }

        /// <summary>
        /// Gets or sets the users trips
        /// </summary>
        [Display(Name="Trips")]
        public virtual IList<Trip> Trips { get; set; }

        /// <summary>
        /// Gets or sets User Role
        /// </summary>
        public virtual string Role { get; set; }

        /// <summary>
        /// Gets or sets Registered datetime
        /// </summary>
        [Display(Name="Registered On")]
        public virtual DateTime Registered { get; set; }

        /// <summary>
        /// Gets or sets last login datetime
        /// </summary>
        [Display(Name = "Last Active")]
        public virtual DateTime LastLogin { get; set; }

        /// <summary>
        /// Gets or sets Invalid Password Date datetime
        /// </summary>
        public virtual DateTime InvalidPasswordDate { get; set; }

        /// <summary>
        /// Gets or sets Invalid Password Count
        /// </summary>
        public virtual int InvalidPassswordCount {get; set;}

        /// <summary>
        /// Gets or sets Salt
        /// </summary>
        [StringLength(256, ErrorMessage="Bad Authentication")]
        public virtual string Salt { get; set; }
    }
}