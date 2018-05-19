using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Licenta.Entityes.InterFacesForViewModel
{
    public interface ILocation
    {
        int IdLocation { get; set; }
        string RegionName { get; set; }
        string StreatName { get; set; }
        int? NrStreat { get; set; }
        int? IdHotel { get; set; }

        Hotels IdHotelNavigation { get; set; }

    }
}