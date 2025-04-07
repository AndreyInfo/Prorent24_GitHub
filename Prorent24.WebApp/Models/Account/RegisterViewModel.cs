using System.ComponentModel.DataAnnotations;

namespace Prorent24.WebApp.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not meet")]
        [DataType(DataType.Password)]
        [Display(Name = "Password confirm")]
        public string PasswordConfirm { get; set; }
    }
}
