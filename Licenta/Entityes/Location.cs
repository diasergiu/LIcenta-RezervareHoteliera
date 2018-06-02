using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class Location
    {
        public Location()
        {
            Hotels = new HashSet<Hotels>();
        }

        public int IdLocation { get; set; }
        public int? NrStreat { get; set; }
        public string StreatName { get; set; }
        public string RegionName { get; set; }
        public string Country { get; set; }

   

        public ICollection<Hotels> Hotels { get; set; }
    }
}
