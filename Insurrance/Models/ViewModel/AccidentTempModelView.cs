using System.ComponentModel;

namespace Insurrance.Models.ViewModel
{
    public class AccidentTempModelView
    {

        [DisplayName("Car Chussis Number")]
        public string carschussis { get; set; }
        public string Notes { get; set; }
        [DisplayName("Car Name")]
        public string Cartype { get; set; }
        [DisplayName("Car Model")]
        public string CarModel { get; set; }
        [DisplayName("Car Color")]
        
        public string Color { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        
    }
}
