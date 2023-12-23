using System.ComponentModel.DataAnnotations;

namespace Insurrance.Models
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        public string Name { get; set; }    
    }
}
