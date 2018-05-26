using Licenta.Entityes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Licenta.ViewModel
{
    public class HotelViewModel
    {
        public Hotels hotel { get; set; }
        public List<IFormFile> HotelPicts { get; set; }


        public int IdHotel { get; set; }
        public string DescriptionTable { get; set; }
        public string HotelName { get; set; }
        public int? Stars { get; set; }

        public ICollection<Employes> Employes { get; set; }
        public ICollection<FacilitiesHotel> FacilitiesHotel { get; set; }
        public ICollection<HotelImages> HotelImages { get; set; }
        public ICollection<Location> Location { get; set; }
        public ICollection<Rooms> Rooms { get; set; }




    }
}