using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Licenta.Entityes
{
    public partial class Reservations
    {

        public int IdReservations { get; set; }

        [Required()]
        public DateTime? CheckIn { get; set; }
        [Required()]
        public DateTime? CheckOut { get; set; }
        public int? IdCustomer { get; set; }
        public int? IdRoom { get; set; }

        public Customers IdCustomerNavigation { get; set; }
        public Rooms IdRoomNavigation { get; set; }
    }
}
