using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

namespace Insurrance.Models
{
    public class CustomerTemp
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        [DisplayName("Customer Name")]
        public string CustName { get; set; }

        [ForeignKey("Nationality")]
        [Required]
        [DisplayName("Nationality")]
        public int Nat { get; set; }

        public Nationality? Nationality { get; set; }


        [Required]
      
        [DisplayName("Date OF Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [MinLength(10), MaxLength(16)]
        [DisplayName("Phone Number 1")]
        [RegularExpression(@"^[\d ]+$")]
        public string PhoneN1 { get; set; }

        [MinLength(10), MaxLength(16)]
        [DisplayName("Phone Number 2")]
        [RegularExpression(@"^[\d ]+$")]
        
        public string? PhoneN2 { get; set; }


        [Required]
        [StringLength(70)]
        [DisplayName("Main Address")]
        public string Address { get; set; }


        [ForeignKey("CustomerStatus")]
        [Required]
        [DisplayName("Customer Status")]
        public int StatusID { get; set; }

        public CustomerStatus? Status { get; set; }

    }
}
