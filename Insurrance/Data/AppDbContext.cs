using Insurrance.Models;
using Insurrance.Models.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Insurrance.Data
{
    public class AppDbContext:IdentityDbContext<AppUserViewModel>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public DbSet<InsuranceType> InsuranceTypes { get; set; }
        public DbSet<Nationality> Nationalites { get; set; }
        public DbSet<CarDetail> CarDetails { get; set; }
        public DbSet<CarDetailTemp> CarDetailsTemp { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerTemp> CustomersTemp { get; set; }
        public DbSet<CustCar> CustCars { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<AccidentTemp> AccidentsTemp { get; set; }
        public DbSet<CarFirstCheck> CarFirstChecks { get; set; }
        public DbSet<CarFirstCheckTemp> CarFirstChecksTemp { get; set; }
        public  DbSet<CarImages> CarImages { get; set; }
        public  DbSet<CarImagesTemp> CarImagesTemp { get; set; }
        public  DbSet<Team> Teams { get; set; }
        public  DbSet<Configuration> Configurations { get; set; }
    }
}
