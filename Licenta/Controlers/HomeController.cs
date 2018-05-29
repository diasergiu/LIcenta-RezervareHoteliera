using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Entityes;
using Licenta.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Licenta.Controlers
{
    public class HomeController : Controller
    {
        private DBRezervareHotelieraContext _context;


        public HomeController(DBRezervareHotelieraContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            HomePageViewModel homePageViewModel = new HomePageViewModel();
            homePageViewModel.listHotels = _context.Hotels.ToList();
            homePageViewModel.listFacilities = _context.Facilities.ToArray();
            homePageViewModel.listLocations = _context.Location.ToList();
            return View(homePageViewModel);
        }

        //[HttpPost("[controller]")]
        [HttpPost]
        //[ActionName("filter")]
        public IActionResult index(int[] IdFacilities, int IdLocation)
        {
            HomePageViewModel _homePageviewmode = new HomePageViewModel();

            if (ModelState.IsValid)
            {
                var LocationHotelQuery = (from H in _context.Hotels
                                          join L in _context.Location
                                          on H.IdHotel equals L.IdHotel
                                          where L.IdLocation == IdLocation
                                          //join FH in _context.FacilitiesHotel
                                          //on H.IdHotel equals FH.IdHotel
                                          //where IdFacilities == (select IdFacilities from _context.FacilitiesHotel)
                                          select H  ).ToList();
                
                foreach (var facilities in IdFacilities)
                {
                    //var LocationHotelQuery = (from H in _context.Hotels
                    //                       join FH in _context.FacilitiesHotel
                    //                       on H.IdHotel equals FH.IdHotel
                    //                       join F in _context.Facilities
                    //                       on FH.IdFacilities equals F.IdFacilities
                    //                       where F.IdFacilities == facilities
                    //                       select H).ToList();
                    LocationHotelQuery = (from H in LocationHotelQuery
                                   join FH in _context.FacilitiesHotel
                                   on H.IdHotel equals FH.IdHotel
                                   where facilities == FH.IdFacilities
                                   select H).ToList();
                }

                _homePageviewmode.listHotels = LocationHotelQuery;
                _homePageviewmode.listFacilities = _context.Facilities.ToArray();
                _homePageviewmode.listLocations = _context.Location.ToList();
                return View(_homePageviewmode);
            }
            _homePageviewmode.listHotels = _context.Hotels.ToList();
            _homePageviewmode.listFacilities = _context.Facilities.ToArray();
            _homePageviewmode.listLocations = _context.Location.ToList();
            return View(_homePageviewmode);
        }
    }
}