using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class HotelImages
    {
        public int IdImageHotel { get; set; }
        public int? IdHotel { get; set; }
        public byte[] ImageHotel { get; set; }

        public Hotels IdHotelNavigation { get; set; }
    }
}
