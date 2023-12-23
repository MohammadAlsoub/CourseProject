using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurrance.Models
{
    public class CarImages
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string carschussis { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}
