using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class Facilities
    {
        public Facilities()
        {
            FacilitiesHotel = new HashSet<FacilitiesHotel>();
        }

        public int IdFacilities { get; set; }
        public string FacilitiesName { get; set; }

        public ICollection<FacilitiesHotel> FacilitiesHotel { get; set; }
    }
}
