
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurrance.Models
{
    public class CarFirstCheckTemp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("Car Chussis Number")]
        public string carschussis { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Front Left")]
        public string FL { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Front Right")]
        public string FR { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Back Left")]
        public string BL { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Back Left")]
        public string BR { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Notes")]
        public string Notes { get; set; }

    }
}
