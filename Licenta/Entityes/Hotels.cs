using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class Hotels
    {
        public Hotels()
        {
            Employes = new HashSet<Employes>();
            FacilitiesHotel = new HashSet<FacilitiesHotel>();
            Location = new HashSet<Location>();
            Rooms = new HashSet<Rooms>();
        }

        public int IdHotel { get; set; }
        public int? Stars { get; set; }
        public string HotelName { get; set; }
        public string DescriptionTable { get; set; }

        public ICollection<Employes> Employes { get; set; }
        public ICollection<FacilitiesHotel> FacilitiesHotel { get; set; }
        public ICollection<Location> Location { get; set; }
        public ICollection<Rooms> Rooms { get; set; }
    }
}
