using System.ComponentModel.DataAnnotations;

namespace Insurrance.Models.ViewModel
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
