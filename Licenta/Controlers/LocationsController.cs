using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta.Entityes;

namespace Licenta.Controlers
{
    public class LocationsController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;

        public LocationsController(DBRezervareHotelieraContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var dBRezervareHotelieraContext = _context.Location.Include(l => l.IdHotelNavigation);
            return View(await dBRezervareHotelieraContext.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .Include(l => l.IdHotelNavigation)
                .SingleOrDefaultAsync(m => m.IdLocation == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "HotelName");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLocation,RegionName,StreatName,NrStreat,IdHotel")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "IdHotel", location.IdHotel);
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location.SingleOrDefaultAsync(m => m.IdLocation == id);
            if (location == null)
            {
                return NotFound();
            }
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "IdHotel", location.IdHotel);
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLocation,RegionName,StreatName,NrStreat,IdHotel")] Location location)
        {
            if (id != location.IdLocation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.IdLocation))
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
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "IdHotel", location.IdHotel);
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .Include(l => l.IdHotelNavigation)
                .SingleOrDefaultAsync(m => m.IdLocation == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Location.SingleOrDefaultAsync(m => m.IdLocation == id);
            _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.IdLocation == id);
        }
    }
}
