using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurrance.Models
{
    public class Configuration
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [DisplayName("Archive Path")]
        public string Path { get; set; }

        [Required]
        [DisplayName("Sender Email")]
        public string CompanyEmail { get; set;}

        [Required]
        public string SMTP { get; set;}

        [Required] 
        public string Port { get; set;}

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Email Password")]
        public string EmailPassword { get; set; }

        [Required]
        [DisplayName("Send Email")]
        public bool Send { get; set; }
/// <summary>
/// ////////////////////////
/// </summary>
        [DisplayName("Wellcome Message")]
        public string Wellcome { get; set; }

        [DisplayName("Car Added Successfully")]
        public string AddedCar { get; set; }

        [DisplayName("Update Info Successfully")]
        public string Update { get; set; }

        [DisplayName("Add Accident")]
        public string Accident { get; set; }

        [DisplayName("Compensation")]
        public string compensation { get; set; }

    }
}
