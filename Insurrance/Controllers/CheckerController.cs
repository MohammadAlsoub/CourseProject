using Insurrance.Customclass;
using Insurrance.Data;
using Insurrance.Models;
using Insurrance.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Insurrance.Controllers
{
    public class CheckerController : Controller
    {
        

        private readonly AppDbContext _context;
        private UserManager<AppUserViewModel> _user;
        public CheckerController(AppDbContext context, UserManager<AppUserViewModel> user)
        {
            _context = context;
            _user = user;
        }

        [Authorize]



        #region Customer
        [HttpGet]
        public async Task<IActionResult> Customer()
        {

            var appDbContext = _context.CustomersTemp.Include(c => c.Status);
            return View(await appDbContext.ToListAsync());
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Customer(CustomerTemp cust)
        {
            var customer = new Customer()
            {
                
                Address = cust.Address,
                CustName=cust.CustName,
                Nat=cust.Nat,
                DOB=cust.DOB,
                Email=cust.Email,
                Status=cust.Status,
                StatusID=cust.StatusID,
                PhoneN1 = cust.PhoneN1, 
                PhoneN2 = cust.PhoneN2  
            };
                
           _context.Add(customer);
                await _context.SaveChangesAsync();
           
            var CustTemp = _context.CustomersTemp.Find(cust.Id);
              _context.Remove(CustTemp);
          _context.SaveChanges();

            EmailClass email = new EmailClass(_context);
            email.SendEmail(customer.Id, 1);

            return RedirectToAction("Customer");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomersTemp == null)
            {
                return NotFound();
            }

            var customer = await _context.CustomersTemp
                .Include(c => c.Nationality).Include(x => x.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var CustTemp = _context.CustomersTemp.Find(id);
            _context.Remove(CustTemp);
            _context.SaveChanges();

            return RedirectToAction("Customer");
           
        }
        #endregion

        #region Cars

        [HttpGet]
        public async Task<IActionResult> Cars()
        {
            var appDbContext = (_context.CarDetailsTemp.Include(c => c.Customer).Include(c => c.InsuranceType));
           
            return View(await appDbContext.ToListAsync());
           
        }
        public async Task<IActionResult> CarDetails(int? id)
        {
            if (id == null || _context.CarDetailsTemp == null)
            {
                return NotFound();
            }

            var carDetail = await _context.CarDetailsTemp
                .Include(c => c.Customer)
                .Include(c => c.InsuranceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carDetail == null)
            {
                return NotFound();
            }
          
            return View(carDetail);
        }
        public async Task<IActionResult> DeleteCar(int id)
        {
            var CarDetailsTemp = _context.CarDetailsTemp.Find(id);
            _context.Remove(CarDetailsTemp);
            _context.SaveChanges();

            return RedirectToAction("Customer");

        }
        [HttpPost]
        public async Task<IActionResult> Cars(CarDetailTemp Car)
        {
            var NewCar = new CarDetail()
            {
               
                Cartype = Car.Cartype,
                ACost = Car.ACost,
                Color = Car.Color,
               
                CustomerNumber = Car.CustomerNumber,
               
                Minstallment = Car.Minstallment,
                ProductionYear = Car.ProductionYear,
                TypeID = Car.TypeID,
                CarModel = Car.CarModel ,
                carschussis = Car.carschussis   
            };
             _context.CarDetails.Add(NewCar);
          
            await _context.SaveChangesAsync();

            EmailClass email = new EmailClass(_context);
            email.SendEmail(NewCar.CustomerNumber, 3);

            return RedirectToAction("DeleteCar",new { id=Car.Id});

        }

        #endregion

        #region Accounting
        #endregion

        #region Assessor

        public async Task<IActionResult> GetcarAccedint()
        {
           var CarAccidentDetail = (from A in _context.AccidentsTemp
                                   join C in _context.CarDetails on A.carschussis equals C.carschussis
                                   select new AccidentTempModelView
                 {
                    CarModel=C.CarModel,
                    carschussis=C.carschussis,
                    Cartype = C.Cartype,
                    Color = C.Color,
                    Notes = A.Notes ,
                    CustomerName=C.Customer.CustName
                    

                 }).AsEnumerable();
            return View(CarAccidentDetail);

        }

        public async Task<IActionResult> GetcarAccedintDetails(string chussis)
        {
             
            var Accident=_context.AccidentsTemp.Where(x=>x.carschussis== chussis).FirstOrDefault();


            List<string> Images = new List<string>();
            var History = _context.CarImagesTemp.Where(x => x.carschussis == chussis);

          
                foreach (var item in History)
                {

                    string DefaultImagePath = item.ImagePath.ToString();
                    byte[] imageArray = System.IO.File.ReadAllBytes(DefaultImagePath);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    Images.Add("data:image/tiff;base64," + base64ImageRepresentation);

                }

            ViewBag.CarImages = Images;
            return View(Accident);
        }


        [HttpPost]
        public  async Task<IActionResult> SaveAccident(AccidentTemp accident)
        {
            var NewAccident=new Accident() { 
            BL=accident.BL, 
            BR=accident.BR,
            carschussis=accident.carschussis,
            FL=accident.FL,
            FR=accident.FR,
            Notes=accident.Notes,
            Level = 1,
            Value=0
            };

            var NewCarImages = _context.CarImagesTemp.Where(x => x.carschussis == accident.carschussis).ToList();
            for (int x=0;x<NewCarImages.Count();x++) {
                var CarImages = new CarImages()
                {
                    carschussis = NewCarImages[x].carschussis,  
                    ImagePath = NewCarImages[x].ImagePath,
                };
               await _context.CarImages.AddAsync(CarImages);
                _context.CarImagesTemp.Remove(NewCarImages[x]);
                await _context.SaveChangesAsync();
               
            }

          
            await _context.Accidents.AddAsync(NewAccident);
            _context.AccidentsTemp.Remove(accident);
            await _context.SaveChangesAsync();


            var customer =  _context.CarDetails.Where(x => x.carschussis == NewAccident.carschussis).FirstOrDefault();
            EmailClass email = new EmailClass(_context);
            email.SendEmail(customer.CustomerNumber, 4);

            return RedirectToAction("GetcarAccedint");
        }


        public async Task<IActionResult> DeleteAccident(int Id)
        {
            var Accedint = await _context.AccidentsTemp.FindAsync(Id);
            var Images =  _context.CarImagesTemp.Where(x => x.carschussis == Accedint.carschussis);
            foreach(var item in Images)
            {
                _context.CarImagesTemp.Remove(item);
                _context.SaveChanges();

            }

            _context.AccidentsTemp.Remove(Accedint);
             _context.SaveChanges();
            return RedirectToAction("GetcarAccedint");
        }
            #endregion
        }
}
