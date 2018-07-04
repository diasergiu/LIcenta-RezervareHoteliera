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

        public async void deleteHotel(int id)
        {
            var facilitiesHotel = context.FacilitiesHotel.Where(x => x.IdHotel == id).ToList();
            foreach (var item in facilitiesHotel)
            {
                context.FacilitiesHotel.Remove(item);
            }

            var employesHotel = context.Employes.Where(x => x.IdHotel == id).ToList();
            foreach (var item in employesHotel)
            {
                context.Employes.Remove(item);
            }
            var roomsHotel = context.Rooms.Where(x => x.IdHotel == id).ToList();
            foreach (var item in roomsHotel)
            {
                DeleteReservations(item.IdRoom);
                context.Rooms.Remove(item);
            }
            var imagesHotel = context.HotelImages.Where(x => x.IdHotel == id).ToList();
            foreach (var item in imagesHotel)
            {
                context.HotelImages.Remove(item);
            }
            var hotels =  context.Hotels.Where(m => m.IdHotel == id).SingleOrDefault();
            context.Hotels.Remove(hotels);
            await context.SaveChangesAsync();
        }

        public void DeleteReservations(int id)
        {
            var reservationsRoom = context.Reservations.Where(x => x.IdRoom == id).ToList();
            foreach(var item in reservationsRoom)
            {
                context.Reservations.Remove(item);
            }
        }

    }
}
