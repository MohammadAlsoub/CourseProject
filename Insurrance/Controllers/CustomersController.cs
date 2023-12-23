
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Insurrance.Data;
using Insurrance.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Insurrance.Models.ViewModel;
using Insurrance.Customclass;

namespace Insurrance.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<AppUserViewModel> _user;
    
        
        public CustomersController(AppDbContext context, UserManager<AppUserViewModel> user)
        {
            _context = context;
            _user = user;
           
          
        }

        // GET: Customers
        
        public async Task<IActionResult> Index()
        {
          

           
                var appDbContext = _context.Customers.Include(c => c.Status);
                return View(await appDbContext.ToListAsync());
            

           
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "Customer Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Nationality).Include(x=>x.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
           
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewBag.Nat = new SelectList(_context.Nationalites, "Id", "NameAR");
            ViewBag.Cstatus = new SelectList(_context.CustomerStatuses, "ID", "Name");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerTemp customer)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(customer);
               await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Nat = new SelectList(_context.Nationalites, "Id", "NameAR", customer.Nat);
            ViewBag.Cstatus = new SelectList(_context.CustomerStatuses, "ID", "Name");
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Edit Customer Data")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewBag.Nat = new SelectList(_context.Nationalites, "Id", "NameAR");
            ViewBag.Cstatus = new SelectList(_context.CustomerStatuses, "ID", "Name");
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                    EmailClass email = new EmailClass(_context);
                    email.SendEmail(customer.Id, 2);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Nat = new SelectList(_context.Nationalites, "Id", "NameAR");
            ViewBag.Cstatus = new SelectList(_context.CustomerStatuses, "ID", "Name");
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Nationality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'AppDbContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            ViewBag.Nat = new SelectList(_context.Nationalites, "Id", "NameAR");
            ViewBag.Cstatus = new SelectList(_context.CustomerStatuses, "ID", "Name");
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        [Route("CustomerSearch")]
        [AllowAnonymous]
        public async Task<List<Customer>> Customers(string Search)
        {

            if (Search == null)
            {
                var appDbContext =await _context.Customers.Include(c => c.Status).ToListAsync();
                return (appDbContext);
            }
            else
            {
                var appDbContext =await _context.Customers.Include(c => c.Status).Where(s =>
               (string.IsNullOrEmpty(Search) || s.CustName.Contains(Search))||
               (string.IsNullOrEmpty(Search) || s.Email.Contains(Search))||
               (string.IsNullOrEmpty(Search) || s.Address.Contains(Search))||
               (string.IsNullOrEmpty(Search) || s.Nationality.NameAR.Contains(Search))||
               (string.IsNullOrEmpty(Search) || s.Nationality.NameEN.Contains(Search))||
               (string.IsNullOrEmpty(Search) || s.PhoneN1.Contains(Search))||
               (string.IsNullOrEmpty(Search) || s.PhoneN2.Contains(Search))||
               (string.IsNullOrEmpty(Search) || s.Status.Name.Contains(Search))
                ).ToListAsync();
                return (appDbContext);
            }

          
        }
    }
}
