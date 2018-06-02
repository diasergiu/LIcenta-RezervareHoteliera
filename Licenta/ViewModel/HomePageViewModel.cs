using Licenta.Entityes;
using Licenta.Entityes.InterFacesForViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.ViewModel
{
    public class HomePageViewModel : PageModel
    {
        //#region Hotel propertys
        //public int IdHotel { get; set; }
        //public int? Stars { get; set; }
        //public string HotelName { get; set; }
        //public string DescriptionTable { get; set; }
        //public ICollection<Employes> Employes { get; set; }
        //public ICollection<FacilitiesHotel> FacilitiesHotel { get; set; }
        //public ICollection<Location> Location { get; set; }
        //public ICollection<Rooms> Rooms { get; set; }

        //#endregion

        //#region Location Propertyes
        //public int IdLocation { get; set; }
        //public string RegionName { get; set; }
        //public string StreatName { get; set; }
        //public int? NrStreat { get; set; }
        //public Hotels IdHotelNavigation { get; set; }

        //int? ILocation.IdHotel { get; set; }
        //#endregion

        //#region Facilities Properties 
        //public int IdFacilities { get; set; }
        //public string FacilitiesName { get; set; }
        //#endregion

        #region Methods
        [BindProperty]
        public List<Hotels> listHotels { get; set; }

        [BindProperty]
        public Facilities[] listFacilities { get; set; }

        [BindProperty]
        public List<Location> listLocations { get; set; }

       

        public int? IdLocation { get; set; }

        public int[] IdFacilities { get; set; }

        


        #endregion
    }
}
