using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Insurrance.Data;
using Insurrance.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Insurrance.Controllers
{
    [Authorize]
    public class CarDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public CarDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CarDetails
        [Authorize(Roles = "View Customer Cars")]
        public async Task<IActionResult> Cars(int id)
        {
            var appDbContext =( _context.CarDetails.Include(c => c.Customer).Include(c => c.InsuranceType)).Where(c =>c.CustomerNumber==id);
            @ViewBag.CustomerNumber = id;
            return View(await appDbContext.ToListAsync());
        }

        // GET: CarDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarDetails == null)
            {
                return NotFound();
            }

            var carDetail = await _context.CarDetails
                .Include(c => c.Customer)
                .Include(c => c.InsuranceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carDetail == null)
            {
                return NotFound();
            }
            ViewBag.Customer = 4;
            return View(carDetail);
        }

        // GET: CarDetails/Create
        [Authorize(Roles = "Add New Car")]
        public IActionResult Create(int Custid)
        {
             
            @ViewBag.CustomerNumber = Custid;
            ViewData["TypeID"] = new SelectList(_context.InsuranceTypes, "ID", "Name");
            return View();
        }

        // POST: CarDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarDetailTemp carDetail,CarFirstCheck firstCheck)
        {
            firstCheck.carschussis = carDetail.carschussis;
            
            if (ModelState.IsValid)
            {
                
               
                    _context.Add(carDetail);
                _context.Add(firstCheck);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Cars", new { id = carDetail.CustomerNumber }  );
                
              
            }
            ViewData["CustomerNumber"] = new SelectList(_context.Customers, "Id", "Id", carDetail.CustomerNumber);
            ViewData["TypeID"] = new SelectList(_context.InsuranceTypes, "ID", "Name", carDetail.TypeID);
            return RedirectToAction("Cars");
        }

        // GET: CarDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarDetails == null)
            {
                return NotFound();
            }

            var carDetail = await _context.CarDetails.FindAsync(id);
            if (carDetail == null)
            {
                return NotFound();
            }
            ViewBag.CustomerNumber = _context.Customers.FindAsync(carDetail.Customer);
            ViewData["TypeID"] = new SelectList(_context.InsuranceTypes, "ID", "Name", carDetail.TypeID);
            return View(carDetail);
        }

        // POST: CarDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cartype,CarModel,ProductionYear,Color,carschussis,TypeID,ACost,Minstallment,CustomerNumber")] CarDetail carDetail)
        {
            if (id != carDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarDetailExists(carDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var CustomerNumber = carDetail.CustomerNumber;
                return RedirectToAction("Cars",new { id= carDetail.CustomerNumber });
            }
            ViewData["CustomerNumber"] = new SelectList(_context.Customers, "Id", "Name", carDetail.CustomerNumber);
            ViewData["TypeID"] = new SelectList(_context.InsuranceTypes, "ID", "Name", carDetail.TypeID);
            return View(carDetail);
        }

        // GET: CarDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarDetails == null)
            {
                return NotFound();
            }

            var carDetail = await _context.CarDetails
                .Include(c => c.Customer)
                .Include(c => c.InsuranceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carDetail == null)
            {
                return NotFound();
            }

            return View(carDetail);
        }

        // POST: CarDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarDetails == null)
            {
                return Problem("Entity set 'AppDbContext.CarDetails'  is null.");
            }
            var carDetail = await _context.CarDetails.FindAsync(id);
            if (carDetail != null)
            {
                _context.CarDetails.Remove(carDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cars));
        }

        private bool CarDetailExists(int id)
        {
          return (_context.CarDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
