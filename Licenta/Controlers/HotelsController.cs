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
    public class HotelsController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;

        public HotelsController(DBRezervareHotelieraContext context)
        {
            _context = context;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            var dBRezervareHotelieraContext = _context.Hotels.Include(h => h.IdLocationNavigation);
            return View(await dBRezervareHotelieraContext.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotels
                .Include(h => h.IdLocationNavigation)
                .SingleOrDefaultAsync(m => m.IdHotel == id);
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "IdLocation");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHotel,DescriptionTable,HotelName,Stars,IdLocation")] Hotels hotels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "IdLocation", hotels.IdLocation);
            return View(hotels);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotels.SingleOrDefaultAsync(m => m.IdHotel == id);
            if (hotels == null)
            {
                return NotFound();
            }
            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "IdLocation", hotels.IdLocation);
            return View(hotels);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHotel,DescriptionTable,HotelName,Stars,IdLocation")] Hotels hotels)
        {
            if (id != hotels.IdHotel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelsExists(hotels.IdHotel))
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
            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "IdLocation", hotels.IdLocation);
            return View(hotels);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotels
                .Include(h => h.IdLocationNavigation)
                .SingleOrDefaultAsync(m => m.IdHotel == id);
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotels = await _context.Hotels.SingleOrDefaultAsync(m => m.IdHotel == id);
            _context.Hotels.Remove(hotels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelsExists(int id)
        {
            return _context.Hotels.Any(e => e.IdHotel == id);
        }
    }
}
