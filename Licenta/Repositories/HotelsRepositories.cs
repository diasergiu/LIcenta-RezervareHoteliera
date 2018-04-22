using Licenta.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories
{
    public class HotelsRepositories
    {
       private DBRezervareHotelieraContext context;

        public HotelsRepositories(DBRezervareHotelieraContext context)
        {
            this.context = context;
        }

        public void CreateHotel(Hotels hotels)
        {
            context.Add(hotels);
            context.SaveChanges();
        }

        public List<Hotels> GetAllHotels()
        {
            return context.Hotels.ToList();
        }

        public Hotels Details(int? id)
        {
            return context.Hotels
                .Where(m => m.IdHotel == id).FirstOrDefault();
        }



    }
}
