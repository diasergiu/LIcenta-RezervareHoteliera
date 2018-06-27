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
    public class HotelImagesController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;

        public HotelImagesController(DBRezervareHotelieraContext context)
        {
            _context = context;
        }

        // GET: HotelImages
        public async Task<IActionResult> Index()
        {
            var dBRezervareHotelieraContext = _context.HotelImages.Include(h => h.IdHotelNavigation);
            return View(await dBRezervareHotelieraContext.ToListAsync());
        }

        // GET: HotelImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelImages = await _context.HotelImages
                .Include(h => h.IdHotelNavigation)
                .SingleOrDefaultAsync(m => m.IdImageHotel == id);
            if (hotelImages == null)
            {
                return NotFound();
            }

            return View(hotelImages);
        }

        // GET: HotelImages/Create
        public IActionResult Create()
        {
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "IdHotel");
            return View();
        }

        // POST: HotelImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdImageHotel,IdHotel,ImageHotel")] HotelImages hotelImages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelImages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "IdHotel", hotelImages.IdHotel);
            return View(hotelImages);
        }

        // GET: HotelImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelImages = await _context.HotelImages.SingleOrDefaultAsync(m => m.IdImageHotel == id);
            if (hotelImages == null)
            {
                return NotFound();
            }
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "IdHotel", hotelImages.IdHotel);
            return View(hotelImages);
        }

        // POST: HotelImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdImageHotel,IdHotel,ImageHotel")] HotelImages hotelImages)
        {
            if (id != hotelImages.IdImageHotel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelImages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelImagesExists(hotelImages.IdImageHotel))
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
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "IdHotel", hotelImages.IdHotel);
            return View(hotelImages);
        }

        // GET: HotelImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelImages = await _context.HotelImages
                .Include(h => h.IdHotelNavigation)
                .SingleOrDefaultAsync(m => m.IdImageHotel == id);
            if (hotelImages == null)
            {
                return NotFound();
            }

            return View(hotelImages);
        }

        // POST: HotelImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotelImages = await _context.HotelImages.Include(x => x.IdHotelNavigation).SingleOrDefaultAsync(m => m.IdImageHotel == id);
            _context.HotelImages.Remove(hotelImages);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit","Hotels",new { id = hotelImages.IdHotelNavigation.IdHotel });
        }

        private bool HotelImagesExists(int id)
        {
            return _context.HotelImages.Any(e => e.IdImageHotel == id);
        }

    }
}
