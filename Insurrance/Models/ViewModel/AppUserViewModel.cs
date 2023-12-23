using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace Insurrance.Models.ViewModel
{
    public class AppUserViewModel:IdentityUser
    {
       
        public int Level { get; set; }

       
        public int TeamID { get; set; }
        public Team? Team { get; set; }

    }
}
