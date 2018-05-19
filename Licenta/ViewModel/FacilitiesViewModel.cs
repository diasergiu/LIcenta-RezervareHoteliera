using Licenta.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Licenta.ViewModel
{
	public class FacilitiesViewModel 
	{
		public int IdFacilities { get; set; }
        public String FacilityName { get; set; }

        public ICollection<FacilitiesHotel> FacilitiesHotel { get; set; }
    }
}