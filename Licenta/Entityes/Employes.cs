using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class Employes
    {
        public int Idemploye { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? IdHotel { get; set; }
        public string EmployType { get; set; }

        public Hotels IdHotelNavigation { get; set; }
    }
}
