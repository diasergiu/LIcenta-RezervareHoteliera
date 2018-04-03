using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class FacilitiesHotel
    {
        public int IdHotel { get; set; }
        public int IdFacilities { get; set; }
        public int? Quantiy { get; set; }

        public Facilities IdFacilitiesNavigation { get; set; }
        public Hotels IdHotelNavigation { get; set; }
    }
}
