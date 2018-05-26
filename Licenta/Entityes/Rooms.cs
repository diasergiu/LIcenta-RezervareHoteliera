using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class Rooms
    {
        public Rooms()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int IdRoom { get; set; }
        public bool? Bath { get; set; }
        public int? Beds { get; set; }
        public int? IdHotel { get; set; }
        public bool? Reserved { get; set; }
        public int? RoomNumber { get; set; }

        public Hotels IdHotelNavigation { get; set; }
        public ICollection<Reservations> Reservations { get; set; }
    }
}
