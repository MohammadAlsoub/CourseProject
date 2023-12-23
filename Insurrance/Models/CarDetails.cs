using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurrance.Models
{
    public class CarDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Car Name")]
        public string Cartype { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Car Model")]
        public string CarModel { get; set; }

        [Required]
        [StringLength(4)]
        [DisplayName("Production Year")]
       
       
        public string ProductionYear { get; set; } 

        [Required]
        [StringLength(50)]
        [DisplayName("Car Color")]
        public string Color { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Car Chussis number")]
        public string carschussis { get; set; }

        [ForeignKey("InsuranceType")]
        [Required]
        [DisplayName("Insurance Product")]
        public int TypeID { get; set; }

        public InsuranceType? InsuranceType { get; set; }


        [Required]

        [DisplayName("Annual Cost")]
        public decimal ACost { get; set; }

        [Required]

        [DisplayName("Monthly installment")]
        public decimal Minstallment { get; set; }


        [ForeignKey("Customer")]
        [Required]
        [DisplayName("Owner")]
        public int CustomerNumber { get; set; }

        public Customer? Customer { get; set; }


    }
}
