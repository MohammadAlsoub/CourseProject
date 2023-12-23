using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Insurrance.Models.ViewModel
{
    public class RegisterViewModel
    {

        public string? Id { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        [Compare("Email", ErrorMessage = "Email Not Match")]
        public string? ConfirmEmail { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password Shoud Be 8 Char")]
        [MaxLength(8, ErrorMessage = "Password Shoud Be 8 Char")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string? ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
        [DisplayName("Level in Team")]
        public int Level { get; set; }
        [DisplayName("User Team name")]
        public int TeamID { get; set; }
        public Team? Team { get; set; }
    }
}
