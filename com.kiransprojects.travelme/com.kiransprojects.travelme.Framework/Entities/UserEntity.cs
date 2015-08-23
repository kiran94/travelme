﻿namespace com.kiransprojects.travelme.Framework.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a User Entity
    /// </summary>
    public class UserEntity : EntityBase
    {
        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        [Display(Name="First Name")]
        [StringLength(100, ErrorMessage="{0} has a maximum length of {1} characters")]
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        [Display(Name="Last Name")]
        [StringLength(100, ErrorMessage = "{0} has a maximum length of {1} characters")]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth
        /// </summary>
        [Display(Name="Date Of Birth")]
        [DataType(DataType.DateTime, ErrorMessage="Please enter a valid date")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        [Display(Name="Email")]
        [StringLength(500, ErrorMessage = "{0} has a maximum length of {1} characters")]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user passwords
        /// </summary>
        [Display(Name="Password")]
        [StringLength(128, ErrorMessage = "{0} has a maximum length of {1} characters")]
        [Required]
        public string UserPassword { get; set; }

        /// <summary>
        /// Gets or sets the profile picture
        /// </summary>
        [Display(Name="Profile Picture")]
        public byte[] ProfilePicture { get; set; }
    }
}