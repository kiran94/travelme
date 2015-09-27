namespace com.kiransprojects.travelme.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Login Model
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        [StringLength(500, ErrorMessage = "{0} has a maximum length of {1} characters")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user passwords
        /// Password must have atleast 1 lower case character
        /// Password must have atleast 1 upper case character
        /// Password must have atleast 1 numerical character
        /// Password must be length 8 - 100
        /// </summary>
        [Required]
        [Display(Name = "Password")]
        [StringLength(256, ErrorMessage = "{0} has a maximum length of {1} characters")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,100}$", ErrorMessage = "{0} must have atleast one number, uppercase character and lowercase character. {0} has a minimum length of 8.")]
        public string UserPassword { get; set; }
    }
}