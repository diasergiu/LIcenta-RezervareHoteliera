using System;
using System.Collections.Generic;

namespace Licenta.Entityes
{
    public partial class CreditCard
    {
        public int IdCard { get; set; }
        public int? IdClient { get; set; }
        public string CardNumber { get; set; }
        public DateTime? CardExpireDate { get; set; }
        public int? Cvc { get; set; }
        public long? MoneyInTheCard { get; set; }

        public Customers IdClientNavigation { get; set; }
    }
}
