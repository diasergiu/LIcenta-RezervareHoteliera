using Licenta.Entityes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Licenta.ViewModel
{
	public class HotelDescriptionPageViewModel 
	{
        [BindProperty]
		public List<Rooms> hotelRooms { get; set; }

        [BindProperty]
        public Hotels hotel { get; set; }
        
	}
}