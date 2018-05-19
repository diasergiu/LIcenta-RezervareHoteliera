using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Licenta.Entityes.InterFacesForViewModel
{
    public interface IFacilities
    {
        int IdFacilities { get; set; }
        string FacilitiesName { get; set; }

        ICollection<FacilitiesHotel> FacilitiesHotel { get; set; }

    }
}