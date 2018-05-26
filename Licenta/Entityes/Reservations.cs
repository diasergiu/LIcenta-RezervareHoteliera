using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class Reservations
    {
        public int IdReservations { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? IdCustomer { get; set; }
        public int? IdRoom { get; set; }

        public Customers IdCustomerNavigation { get; set; }
        public Rooms IdRoomNavigation { get; set; }
    }
}
