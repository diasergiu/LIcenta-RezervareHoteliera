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
        public async Task<IActionResult> Details(int? id, DateTime checkInDate, DateTime checkOutDate)
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




            //_Hotel.Rooms =
            var rooms = _context.Rooms.Where(x => x.IdHotel == id).ToList();

            foreach (var room in rooms)
            {
                var reservations = _context.Reservations.Where(x => x.IdRoom == room.IdRoom)
                    .OrderBy(x => x.CheckOut).ToList();
                bool found = checkOutDate < reservations[0].CheckIn
                    || checkInDate > reservations[reservations.Count() - 1].CheckOut;

                for (int i = 0; i < reservations.Count() - 2; ++i)
                {

                    if (reservations[i].CheckOut < checkInDate && reservations[i + 1].CheckIn > checkOutDate)
                    //   checkInDate > reservations[i].CheckOut   && checkOutDate > reservations[i].CheckOut   && checkInDate > reservations[i].CheckOut ||
                    //   reservations[i].CheckIn < checkOutDate && reservations[i].CheckOut < checkOutDate && reservations[i].CheckOut < checkInDate)
                    //   if(!(reservations[i].CheckIn < checkInDate && reservations[i].CheckOut > checkOutDate))
                    {
                        found = true;
                        break;
                    }
                    if (found) _Hotel.Rooms.Add(room);

                }
                if (found) _Hotel.Rooms.Add(room);

            }
            var IdCustommer = HttpContext.Session.GetString("IdCustomer");

            _Hotel.IdUser = Convert.ToInt32(IdCustommer);

            _Hotel.CheckIn = checkInDate;
            _Hotel.CheckOut = checkOutDate;
            return View(_Hotel);
        }





        // GET: Hotels/Create
        public IActionResult Create()
        {
            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "RegionName");
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
            var facilities = _context.Facilities.ToArray();
            //change facilities if they ar pozitive or not 
            var facilitiesHotel = _context.FacilitiesHotel.Where(x => x.IdHotel == hotels.IdHotel).ToList();
            for (int i = 0; i < facilities.Length; i++)
            {
                foreach (var hotel_facility in facilitiesHotel)
                {
                    if (facilities[i].IdFacilities == hotel_facility.IdFacilities)
                    {
                        facilities[i].IsChecked = true;
                    }
                }

            }
            HotelPass.facilities = facilities;


            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "IdLocation", hotels.IdLocation);
            return View(HotelPass);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHotel,DescriptionTable,HotelName,Stars,IdLocation,ImageHotel,facilities")] HotelCreateViewModel hotels/*, [Bind("ImageHotel")] List<IFormFile> ImageHotel,[Bind("facilities")] Facilities[] facilitiesSelectedForHotel*/)
        {
            if (id != hotels.IdHotel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                int _IdHotelImage = _context.HotelImages.Last().IdImageHotel;
                //the save image part 
                if (!(hotels.ImageHotel == null))
                    {


                    foreach (var image in hotels.ImageHotel)
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
                }
                // facilities part of the code
                for (int i = 0; i < hotels.facilities.Length; i++)
                {
                    //if check treat the exercite
                    if (hotels.facilities[i].IsChecked)
                    {                      
                        if (_context.FacilitiesHotel.Where(x => x.IdHotel == hotels.IdHotel &&
                        x.IdFacilities == hotels.facilities[i].IdFacilities).FirstOrDefault() == null)
                        {
                            FacilitiesHotel facilities = new FacilitiesHotel()
                            {
                                IdFacilities = hotels.facilities[i].IdFacilities,
                                IdHotel = hotels.IdHotel
                            };
                            _context.FacilitiesHotel.Add(facilities);
                        }
                    }
                    //if the checkbox is not checked
                    else
                    {
                        var uncheckedBox = _context.FacilitiesHotel.Where(x => x.IdHotel == hotels.IdHotel && x.IdFacilities == hotels.facilities[i].IdFacilities).FirstOrDefault();
                        if(uncheckedBox == null)
                        {

                        }
                        else
                        {
                            _context.FacilitiesHotel.Remove(uncheckedBox);
                        }
                    }
                }


                Hotels hotelUpdate = new Hotels()
                {
                    IdHotel = id,
                    DescriptionTable = hotels.DescriptionTable,
                    HotelName = hotels.HotelName,
                    Stars = hotels.Stars,
                    IdLocation = hotels.IdLocation
                };
                // the edit hotel part 
                try
                {
                    _context.Update(hotelUpdate);
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
