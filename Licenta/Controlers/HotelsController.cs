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
using Licenta.Repositories;

namespace Licenta.Controlers
{
    public class HotelsController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;
        private HotelsRepositories hotelRepositories;

        public HotelsController(DBRezervareHotelieraContext context)
        {
            _context = context;
            hotelRepositories = new HotelsRepositories(context);
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            var dBRezervareHotelieraContext = _context.Hotels.Include(h => h.IdLocationNavigation);
            return View(await dBRezervareHotelieraContext.ToListAsync());
        }


        // GET: Hotels/Details/5
        public async Task<IActionResult> DetailsHotel(int? id, DateTime checkInDate, DateTime checkOutDate)
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
            HotelDescriptionPageViewModel _Hotel = hotelRepositories.GetHotelDescriptionViewModel(hotels);
                        
            
            for (int i = 1; i < _Hotel.GaleryImages.Length; i++)
                for (int y = 0; y < _Hotel.GaleryImages[i].ImageHotel.Length; y++)
                    _Hotel.GaleryImages[i].ImageHotel.ToString();

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
            var IdCustommer = HttpContext.Session.GetInt32("IdCustomer");

            _Hotel.IdUser = Convert.ToInt32(IdCustommer);

            _Hotel.CheckIn = checkInDate;
            _Hotel.CheckOut = checkOutDate;
            return View(_Hotel);
        }





        // GET: Hotels/Create
        public IActionResult CreateHotel()
        {
            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "RegionName");
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHotel([Bind("IdHotel,DescriptionTable,HotelName,Stars,IdLocation,ImageHotel")] Hotels hotels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "RegionName", hotels.IdLocation);
            return View(hotels);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> EditHotel(int? id)
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
            HotelCreateViewModel HotelPass = hotelRepositories.GetHotelCreateViewModel(hotels);
            
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


            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "RegionName", hotels.IdLocation);
            return View(HotelPass);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHotel(int id, [Bind("IdHotel,DescriptionTable,HotelName,Stars,IdLocation,ImageHotel,facilities")] HotelCreateViewModel hotels/*, [Bind("ImageHotel")] List<IFormFile> ImageHotel,[Bind("facilities")] Facilities[] facilitiesSelectedForHotel*/)
        {
            if (id != hotels.IdHotel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                int _IdHotelImage = _context.HotelImages.Last().IdImageHotel;
                 
                if (!(hotels.ImageHotel == null))
                {
                    hotelRepositories.SaveImages(hotels.ImageHotel, hotels.IdHotel, _IdHotelImage);
                }

                hotelRepositories.SaveChangesFacilities(hotels.facilities, hotels.IdHotel);
               
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

                hotels.GaleryImages = _context.HotelImages.Where(x => x.IdHotel == hotels.IdHotel).ToArray();
                hotels.imagesString = new string[hotels.GaleryImages.Length];
                //new List<string>();
                int imageNumber = 0;
                foreach (var item in hotels.GaleryImages)
                {
                    hotels.imagesString[imageNumber] = (Convert.ToBase64String(item.ImageHotel));
                    imageNumber++;
                }
            }
            ViewData["IdLocation"] = new SelectList(_context.Location, "IdLocation", "RegionName", hotels.IdLocation);
            return View(hotels);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> DeleteHotel(int? id)
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
        [HttpPost, ActionName("DeleteHotel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HotelsRepositories hr = new HotelsRepositories(_context);
            hr.deleteHotel(id);

            


            return RedirectToAction(nameof(Index));
        }

        private bool HotelsExists(int id)
        {
            return _context.Hotels.Any(e => e.IdHotel == id);
        }



    }
}
