using System.ComponentModel.DataAnnotations;

namespace Prorent24.WebApp.Models.Account
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not meet")]
        public string ConfirmNewPassword { get; set; }
    }
}
