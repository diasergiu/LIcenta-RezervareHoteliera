using Licenta.Controlers;
using Licenta.Entityes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestLicenta
{
    [TestClass]
    class HotelControlTest
    {
        DBRezervareHotelieraContext context = new DBRezervareHotelieraContext();

        [TestMethod]
        public void AddToTheDataBase()
        {
            HotelsController hotel = new HotelsController(context);
            //await hotel.AddHotel();


        } 
    }
}
