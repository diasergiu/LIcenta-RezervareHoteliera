using Licenta.Entityes;
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
        public byte[] ImageHotel { get; set; }

        public List<Byte[]> ImagesHotel { get; set; }
        
    }
}