using Insurrance.Data;
using Insurrance.Models;
using Insurrance.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace Insurrance.Controllers
{
    [Authorize]
    public class AccedintController : Controller
    {
        private readonly AppDbContext _context;
      
        public AccedintController(AppDbContext context)
        {

            _context = context;
        }
        public IActionResult AddAccedint(string CHSNum)
        {

            ViewBag.chsnum= CHSNum;
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAccedint(AccidentTemp Acc,IEnumerable<IFormFile> files)
        {
            //var dir = @"D:\WorkSpace\ASPCourse\Archive\";
            var dir = _context.Configurations.First().Path;
            
            

            
           
            foreach (var item in files)
            {
                Guid guid = Guid.NewGuid();
                using (var filestream = new FileStream(Path.Combine(dir, guid.ToString()+ ".tiff"), FileMode.Create, FileAccess.Write))
                {
                    item.CopyTo(filestream);
                  
                    CarImagesTemp carImages = new CarImagesTemp() { 
                    
                        carschussis=Acc.carschussis,
                        ImagePath= dir+ guid.ToString() + ".tiff"
                    };
                    await _context.AddAsync(carImages); 
                    await _context.SaveChangesAsync();

                }
            }
            await _context.AccidentsTemp.AddAsync(Acc);
            await _context.SaveChangesAsync();

            
         
                return RedirectToAction("Index", "Customers");
        }



        public  IActionResult getCarHistory(string ShasiNumber) {



            var History =  _context.CarImages.Where(x=>x.carschussis == ShasiNumber);
            var Check = _context.Accidents.Where(x => x.carschussis == ShasiNumber).FirstOrDefault();
            var FirstCheck = _context.CarFirstChecks.Where(x => x.carschussis == ShasiNumber).FirstOrDefault();
            var carDeatail = _context.CarDetails.Include(x => x.Customer).Include(x=>x.InsuranceType).Where(x => x.carschussis == ShasiNumber).FirstOrDefault();


            if (Check != null)
            {
                FirstCheck.carschussis=Check.carschussis;  
                FirstCheck.FR=Check.FR; 
                FirstCheck.FL=Check.FL;
                FirstCheck.BL=Check.BL;
                FirstCheck.BR=Check.BR;
                FirstCheck.Notes=Check.Notes;
            }
           var car= new CarsViewModel
            {
                CarDetails = carDeatail,
                CarFirstCheck = FirstCheck,
              

           };


            List<string> Images = new List<string>();


            if (History == null)
            {

                return NotFound();
            }
            else
            {

               
                foreach (var item in History) {

                    string DefaultImagePath = item.ImagePath.ToString();
                    byte[] imageArray = System.IO.File.ReadAllBytes(DefaultImagePath);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                     Images.Add("data:image/tiff;base64," + base64ImageRepresentation);
                 
                }

               
               
            }

          
         
                ViewBag.CarImages = Images;
               return View("CarWihtoutHistory", car);

          
          
          

        } 
    }
}
