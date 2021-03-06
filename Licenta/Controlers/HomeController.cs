﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Entityes;
using Licenta.ViewModel;
using Microsoft.AspNetCore.Http;
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
            //homePageViewModel.listHotels = _context.Hotels.ToList();
            homePageViewModel.CheckIn = DateTime.Now;
            
            homePageViewModel.CheckOut = homePageViewModel.CheckIn.AddDays(2); 
            homePageViewModel.listFacilities = _context.Facilities.ToArray();
            homePageViewModel.listLocations = _context.Location.ToList();
            return View(homePageViewModel);
        }

        [HttpGet]
        public IActionResult HomePage(Facilities[] listFacilities, int IdLocation, DateTime CheckIn, DateTime CheckOut)
        {
            HomePageViewModel _homePageviewmode = new HomePageViewModel();

            if (ModelState.IsValid)
            {
                List<Hotels> LocationHotelQuery = new List<Hotels>();
                if (IdLocation != 0)
                { 
                LocationHotelQuery = (from H in _context.Hotels
                                          where H.IdLocation == IdLocation
                                          select H).ToList();
                }
                else
                {
                    LocationHotelQuery = _context.Hotels.ToList();
                }
                List<int> SelectedFacilities = new List<int>();
                for(int i = 0; i < listFacilities.Length; i++)
                {
                    if (listFacilities[i].IsChecked == true)
                    {
                        SelectedFacilities.Add(listFacilities[i].IdFacilities);
                    }
                }
                foreach(var item in SelectedFacilities)
                {
                    LocationHotelQuery = (from H in LocationHotelQuery
                                          join FH in _context.FacilitiesHotel
                                          on H.IdHotel equals FH.IdHotel
                                          where item == FH.IdFacilities
                                          select H).ToList();
                }
                HttpContext.Session.SetInt32("Location", IdLocation);  

                _homePageviewmode.CheckIn = CheckIn;
                _homePageviewmode.CheckOut = CheckOut;
                _homePageviewmode.listHotels = LocationHotelQuery;
                _homePageviewmode.listFacilities = _context.Facilities.ToArray();
                _homePageviewmode.listLocations = _context.Location.ToList();
                return View(_homePageviewmode);
                
            }        
            _homePageviewmode.listHotels = _context.Hotels.ToList();
            _homePageviewmode.listFacilities = _context.Facilities.ToArray();
            _homePageviewmode.listLocations = _context.Location.ToList();
            return View(_homePageviewmode);
            //return RedirectToRoute("Home/HomePage", _homePageviewmode)
        }
    }
}