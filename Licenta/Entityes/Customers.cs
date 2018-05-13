using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Licenta.Entityes
{
    public partial class Customers
    {
        public Customers()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int IdCustomer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Compare("Password", ErrorMessage = "passwords not match.")]
        //[DataType(DataType.Password)]
        //public string VerifyPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Reservations> Reservations { get; set; }
    }
}
