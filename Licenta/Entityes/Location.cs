using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class Location
    {
        public int IdLocation { get; set; }
        public string RegionName { get; set; }
        public string StreatName { get; set; }
        public int? NrStreat { get; set; }
        public int? IdHotel { get; set; }

        public Hotels IdHotelNavigation { get; set; }
    }
}
