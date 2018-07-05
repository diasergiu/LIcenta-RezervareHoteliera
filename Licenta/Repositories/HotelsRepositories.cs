using Licenta.Entityes;
using Licenta.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Repositories
{
    public class HotelsRepositories
    {
       private DBRezervareHotelieraContext context;

        public HotelsRepositories(DBRezervareHotelieraContext _context)
        {
            this.context = _context;
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
        
        public void SaveImages(List<IFormFile> ImageHotel,int idHotel, int IdImage)
        {

            foreach (var image in ImageHotel)
            {
                IdImage++;
                HotelImages Immage = new HotelImages()
                {
                    IdHotel =  idHotel,
                    IdImageHotel = IdImage
                };
                using (var memoryStream = new MemoryStream())
                {
                    image.CopyToAsync(memoryStream);
                    Immage.ImageHotel = memoryStream.ToArray();
                }
                context.HotelImages.Add(Immage);

            }
        }


        public void SaveChangesFacilities(Facilities[] facilities, int IdHotel)
        {
            for (int i = 0; i < facilities.Length; i++)
            {
                //if check treat the exercite
                if (facilities[i].IsChecked)
                {
                    if (context.FacilitiesHotel.Where(x => x.IdHotel == IdHotel &&
                    x.IdFacilities == facilities[i].IdFacilities).FirstOrDefault() == null)
                    {
                        FacilitiesHotel _facilities = new FacilitiesHotel()
                        {
                            IdFacilities = facilities[i].IdFacilities,
                            IdHotel = IdHotel
                        };
                        context.FacilitiesHotel.Add(_facilities);
                    }
                }
                //if the checkbox is not checked
                else
                {
                    var uncheckedBox = context.FacilitiesHotel.Where(x => x.IdHotel == IdHotel && x.IdFacilities == facilities[i].IdFacilities).FirstOrDefault();
                    if (uncheckedBox == null)
                    {

                    }
                    else
                    {
                        context.FacilitiesHotel.Remove(uncheckedBox);
                    }
                }
            }
        }
        // end of code 

        public HotelCreateViewModel GetHotelCreateViewModel(Hotels hotels)
        {
            HotelCreateViewModel HotelPass = new HotelCreateViewModel()
            {
                IdHotel = hotels.IdHotel,
                HotelName = hotels.HotelName,
                DescriptionTable = hotels.DescriptionTable,
                Stars = hotels.Stars,
                IdLocation = hotels.Stars,
                GaleryImages = context.HotelImages.Where(x => x.IdHotel == hotels.IdHotel).ToArray()

            };

            HotelPass.imagesString = new string[HotelPass.GaleryImages.Length];

            int imageNumber = 0;
            foreach (var item in HotelPass.GaleryImages)
            {
                HotelPass.imagesString[imageNumber] = (Convert.ToBase64String(item.ImageHotel));
                imageNumber++;
            }

            return HotelPass;
        }

        public HotelDescriptionPageViewModel GetHotelDescriptionViewModel(Hotels hotels)
        {
            HotelDescriptionPageViewModel hotelsDescriptionPageViewModel = new HotelDescriptionPageViewModel()
            {
                IdHotel = hotels.IdHotel,
                HotelName = hotels.HotelName,
                DescriptionTable = hotels.DescriptionTable,
                Stars = hotels.Stars,
                IdLocationNavigation = hotels.IdLocationNavigation,

                GaleryImages = context.HotelImages.Where(x => x.IdHotel == hotels.IdHotel).ToArray(),

            };

            hotelsDescriptionPageViewModel.imagesString = new string[hotelsDescriptionPageViewModel.GaleryImages.Length];

            int imageNumber = 0;
            foreach (var item in hotelsDescriptionPageViewModel.GaleryImages)
            {
                hotelsDescriptionPageViewModel.imagesString[imageNumber] = (Convert.ToBase64String(item.ImageHotel));
                imageNumber++;
            }

            return hotelsDescriptionPageViewModel;
        }
    }
}
