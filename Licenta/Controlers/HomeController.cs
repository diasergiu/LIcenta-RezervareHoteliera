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
        [ActionName("filter")]
        public IActionResult Index ([Bind("IdFacilities,IdLocation")] HomePageViewModel HPVM)
        {
            if (ModelState.IsValid)
            {


                HomePageViewModel _homePageviewmode = new HomePageViewModel();

                var LocationHotelQuery = (from H in _context.Hotels
                                          join L in _context.Location
                                          on H.IdHotel equals L.IdHotel
                                          where L.IdLocation == HPVM.IdLocation
                                          select H).ToList();
                foreach (var facilities in HPVM.IdFacilities)
                {
                    LocationHotelQuery = (from H in _context.Hotels
                                          join FH in _context.FacilitiesHotel
                                          on H.IdHotel equals FH.IdHotel
                                          join F in _context.Facilities
                                          on FH.IdFacilities equals F.IdFacilities
                                          where F.IdFacilities == facilities
                                          select H).ToList();
                }

                _homePageviewmode.listHotels = LocationHotelQuery;
                _homePageviewmode.listFacilities = _context.Facilities.ToArray();
                _homePageviewmode.listLocations = _context.Location.ToList();
                return View(_homePageviewmode);
            }
            return Redirect("index");
        }
    }
}