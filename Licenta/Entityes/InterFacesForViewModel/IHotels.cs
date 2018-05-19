using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Licenta.Entityes.InterFacesForViewModel
{
    public interface IHotels 
    {

        int IdHotel { get; set; }
        int? Stars { get; set; }
        string HotelName { get; set; }
        string DescriptionTable { get; set; }

        ICollection<Employes> Employes { get; set; }
        ICollection<FacilitiesHotel> FacilitiesHotel { get; set; }
        ICollection<Location> Location { get; set; }
        ICollection<Rooms> Rooms { get; set; }

    }
}