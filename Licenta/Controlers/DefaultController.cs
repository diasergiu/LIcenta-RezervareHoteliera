using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Licenta.Entityes;
using Microsoft.AspNetCore.Mvc;

namespace Licenta.Controlers
{
    public class DefaultController : Controller
    {
        private DBRezervareHotelieraContext context;

        public DefaultController()
        {
            context = new DBRezervareHotelieraContext(new Microsoft.EntityFrameworkCore.DbContextOptions<DBRezervareHotelieraContext>());
        }
        public IActionResult Index()
        {
            var ListaHoteluri = context.Hotels.ToList();
            return View(ListaHoteluri);
        }

        //public Task<IActionResult> filter()
        //{

        //}

        //public List<Hotels> getHotelsAfterFacilities()
        //{
        //    var ListaHotel = context.Hotels.
        //}
    }
}