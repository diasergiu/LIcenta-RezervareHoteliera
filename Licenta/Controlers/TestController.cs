using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Entityes;
using Microsoft.AspNetCore.Mvc;

namespace Licenta.Controlers
{
    public class TestController : Controller
    {
        DBRezervareHotelieraContext context ;

        //public TestController()
        //{
        //    context = new DBRezervareHotelieraContext();
        //}

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void testCreateHotel([Bind("Stars,HotelName,DescriptionTable")] Hotels hotelPost)
        {
            context.Add(hotelPost);
            context.SaveChanges();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHotel,Stars,HotelName,DescriptionTable")] Hotels hotels)
        {
            if (ModelState.IsValid)
            {
                context.Add(hotels);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotels);
        }
    }
}