using Licenta.Entityes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Licenta.ViewModel
{
	public class HotelCreateViewModel 
	{
        public int IdHotel { get; set; }
        public string DescriptionTable { get; set; }
        public string HotelName { get; set; }
        public int? Stars { get; set; }
        public int? IdLocation { get; set; }

        public Location IdLocationNavigation { get; set; }
        public ICollection<Employes> Employes { get; set; }
        public ICollection<FacilitiesHotel> FacilitiesHotel { get; set; }
        public ICollection<HotelImages> HotelImages { get; set; }
        public ICollection<Rooms> Rooms { get; set; }

        public int IdImageHotel { get; set; }
      //  public byte[] ImageHotel { get; set; }

        public List<Byte[]> ImagesHotel { get; set; }
        
        public List<IFormFile> ImageHotel { get; set; }

        public Facilities[] facilities { get; set; }

        public HotelImages[] GaleryImages { get; set; }

        public string[] imagesString { get; set; }
    }
}