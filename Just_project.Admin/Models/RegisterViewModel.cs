using System.ComponentModel.DataAnnotations;

namespace Just_project.Admin.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; } // From register form

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty; // From register form

        // Optional fields from your form
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty; // From register form

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty; // From register form

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty; // Corresponds to name="rpassword"

        [Required(ErrorMessage = "You must agree to the terms")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the Terms of Service and Privacy Policy.")]
        [Display(Name = "I agree to the Terms of Service & Privacy Policy")]
        public bool AgreeToTerms { get; set; } // Corresponds to name="tnc"
    }
}
