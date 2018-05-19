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
        private DBRezervareHotelieraContext context;
        [BindProperty]
        public List<Facilities> Facilities { get; set; }

        public HomeController(DBRezervareHotelieraContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {

            HomePageViewModel homePageViewModel = new HomePageViewModel();
            homePageViewModel.listHotels = context.Hotels.ToList();
            homePageViewModel.listFacilities = context.Facilities.ToArray();
            homePageViewModel.listLocations = context.Location.ToList();
            ViewData["IdLocation"] = new SelectList(homePageViewModel.listLocations, "IdLocation", "LocationName");
            return View(homePageViewModel);
        }

        [HttpPost]
        public IActionResult Index([FromBody] HomePageViewModel HPVM)
        {
            if (ModelState.IsValid)
            {


                HomePageViewModel _homePageviewmode = new HomePageViewModel();

                var LocationHotelQuery = (from H in context.Hotels
                                          join L in context.Location
                                          on H.IdHotel equals L.IdHotel
                                          where L.IdLocation == HPVM.IdLocation
                                          select H).ToList();
                foreach (var facilities in HPVM.IdFacilities)
                {
                    LocationHotelQuery = (from H in context.Hotels
                                          join FH in context.FacilitiesHotel
                                          on H.IdHotel equals FH.IdHotel
                                          join F in context.Facilities
                                          on FH.IdFacilities equals F.IdFacilities
                                          where F.IdFacilities == facilities
                                          select H).ToList();
                }

                _homePageviewmode.listHotels = LocationHotelQuery;
                _homePageviewmode.listFacilities = context.Facilities.ToArray();
                _homePageviewmode.listLocations = context.Location.ToList();
                return View(_homePageviewmode);
            }
            return Redirect("index");
        }
    }
}