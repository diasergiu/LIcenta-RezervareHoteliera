using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Licenta.Entityes
{
    public class EmployesController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;

        public EmployesController(DBRezervareHotelieraContext context)
        {
            _context = context;
        }

        // GET: Employes
        public async Task<IActionResult> Index()
        {
            var dBRezervareHotelieraContext = _context.Employes.Include(e => e.IdHotelNavigation);
            return View(await dBRezervareHotelieraContext.ToListAsync());
        }

        // GET: Employes/Details/5
        public async Task<IActionResult> DetailsEmploye(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employes = await _context.Employes
                .Include(e => e.IdHotelNavigation)
                .SingleOrDefaultAsync(m => m.Idemploye == id);
            if (employes == null)
            {
                return NotFound();
            }

            return View(employes);
        }

        // GET: Employes/Create
        public IActionResult CreateEmploye()
        {
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "HotelName");
            return View();
        }

        // POST: Employes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmploye([Bind("Idemploye,EmployType,FirstName,IdHotel,LastName,Password,Username")] Employes employes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "HotelName", employes.IdHotel);
            return View(employes);
        }

        // GET: Employes/Edit/5
        public async Task<IActionResult> EditEmploye(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employes = await _context.Employes.SingleOrDefaultAsync(m => m.Idemploye == id);
            if (employes == null)
            {
                return NotFound();
            }
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "HotelName", employes.IdHotel);
            return View(employes);
        }

        // POST: Employes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmploye(int id, [Bind("Idemploye,EmployType,FirstName,IdHotel,LastName,Password,Username")] Employes employes)
        {
            if (id != employes.Idemploye)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployesExists(employes.Idemploye))
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
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "HotelName", employes.IdHotel);
            return View(employes);
        }

        // GET: Employes/Delete/5
        public async Task<IActionResult> DeleteEmploye(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employes = await _context.Employes
                .Include(e => e.IdHotelNavigation)
                .SingleOrDefaultAsync(m => m.Idemploye == id);
            if (employes == null)
            {
                return NotFound();
            }

            return View(employes);
        }

        // POST: Employes/Delete/5
        [HttpPost, ActionName("DeleteEmploye")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employes = await _context.Employes.SingleOrDefaultAsync(m => m.Idemploye == id);
            _context.Employes.Remove(employes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployesExists(int id)
        {
            return _context.Employes.Any(e => e.Idemploye == id);
        }
    }
}
