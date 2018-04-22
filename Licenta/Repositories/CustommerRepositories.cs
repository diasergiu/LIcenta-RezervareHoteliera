using Licenta.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories
{
    public class CustommerRepositories
    {
       private DBRezervareHotelieraContext context;

        public CustommerRepositories(DBRezervareHotelieraContext _context)
        {
            context = _context;
        }


    }
}
