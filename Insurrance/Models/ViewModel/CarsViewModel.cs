using Microsoft.EntityFrameworkCore;

namespace Insurrance.Models.ViewModel
{
    public class CarsViewModel
    {

        public CarDetail CarDetails { get; set; }
        public CarFirstCheck CarFirstCheck { get; set; }

        //public Accident Accident { get; set; }
    }
}
