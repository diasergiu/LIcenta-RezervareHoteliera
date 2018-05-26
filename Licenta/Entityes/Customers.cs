using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class Customers
    {
        public Customers()
        {
            CreditCard = new HashSet<CreditCard>();
            Reservations = new HashSet<Reservations>();
        }

        public int IdCustomer { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }

        public ICollection<CreditCard> CreditCard { get; set; }
        public ICollection<Reservations> Reservations { get; set; }
    }
}
