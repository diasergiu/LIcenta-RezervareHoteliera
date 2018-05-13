using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Entityes;
using Microsoft.AspNetCore.Mvc;

namespace Licenta.Controlers
{
    public class HomeController : Controller
    {
        private DBRezervareHotelieraContext context;

        public HomeController(DBRezervareHotelieraContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View(context);
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(string Location, List<Boolean> SelectedFacilities)
        //{
        //    var Hotel = from m in context.Hotels select m;


        //}
    }
}