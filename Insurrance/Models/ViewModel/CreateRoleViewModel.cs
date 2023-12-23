using System.ComponentModel.DataAnnotations;

namespace Insurrance.Models.ViewModel
{
    public class CreateRoleViewModel
    {

        [Required]
        public string RoleName { get; set; }
    }
}
