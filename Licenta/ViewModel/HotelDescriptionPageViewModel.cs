using Licenta.Entityes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Licenta.ViewModel
{
	public class HotelDescriptionPageViewModel 
	{
        public HotelDescriptionPageViewModel()
        {
            Employes = new HashSet<Employes>();
            FacilitiesHotel = new HashSet<FacilitiesHotel>();
            HotelImages = new HashSet<HotelImages>();
            Rooms = new HashSet<Rooms>();
        }

        public int IdHotel { get; set; }
        public string DescriptionTable { get; set; }
        public string HotelName { get; set; }
        public int? Stars { get; set; }
        public int? IdLocation { get; set; }

        [Required()]
        public DateTime CheckIn { get; set; }
        [Required()]
        public DateTime CheckOut { get; set; }
        public int IdUser { get; set; }

        public Location IdLocationNavigation { get; set; }
        public ICollection<Employes> Employes { get; set; }
        public ICollection<FacilitiesHotel> FacilitiesHotel { get; set; }
        public ICollection<HotelImages> HotelImages { get; set; }
        public ICollection<Rooms> Rooms { get; set; }

        public HotelImages[] GaleryImages { get; set; }

        public string[] imagesString { get; set; }
     
        public List<CreditCard> CreditCardsClient { get; set; }

        public int IdCard { get; set; }
    }
}