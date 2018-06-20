using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta.Entityes;
using Licenta.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO;

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
            HotelDescriptionPageViewModel _Hotel = new HotelDescriptionPageViewModel()
            {
                IdHotel = hotels.IdHotel,
                HotelName = hotels.HotelName,
                DescriptionTable = hotels.DescriptionTable,
                Stars = hotels.Stars,
                IdLocationNavigation = hotels.IdLocationNavigation,

                GaleryImages = _context.HotelImages.Where(x => x.IdHotel == hotels.IdHotel).ToList(),
                 
            };
            _Hotel.imagesString = new List<string>();
            foreach (var item in _Hotel.GaleryImages)
                _Hotel.imagesString.Add(Convert.ToBase64String(item.ImageHotel));




            _Hotel.Rooms = _context.Rooms.Where(x => x.IdHotel == id).ToList();
            var IdCustommer = HttpContext.Session.GetString("IdCustomer");
             
            _Hotel.IdUser = Convert.ToInt32(IdCustommer);
          
            
            return View(_Hotel);
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
        public async Task<IActionResult> Create([Bind("IdHotel,DescriptionTable,HotelName,Stars,IdLocation,ImageHotel")] Hotels hotels)
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
            HotelCreateViewModel HotelPass = new HotelCreateViewModel()
            {
                IdHotel = hotels.IdHotel,
                HotelName = hotels.HotelName,
                DescriptionTable = hotels.DescriptionTable,
                Stars = hotels.Stars,
                IdLocation = hotels.Stars
            };
            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "IdLocation", hotels.IdLocation);
            return View(HotelPass);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHotel,DescriptionTable,HotelName,Stars,IdLocation")] Hotels hotels, [Bind("ImageHotel")] List<IFormFile> ImageHotel)
        {
            if (id != hotels.IdHotel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                int _IdHotelImage = _context.HotelImages.Last().IdImageHotel;
                //the save image part 
                foreach (var image in ImageHotel)
                {
                    _IdHotelImage++;
                    HotelImages Immage = new HotelImages()
                    {
                        IdHotel = hotels.IdHotel,
                        IdImageHotel = _IdHotelImage
                    };
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        Immage.ImageHotel = memoryStream.ToArray();
                    }
                    _context.HotelImages.Add(Immage);
                }

                // the edit hotel part 
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
