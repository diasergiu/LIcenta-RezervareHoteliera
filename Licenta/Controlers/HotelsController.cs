using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta.Entityes;
using Licenta.Repositories;

namespace Licenta.Controlers
{
    public class HotelsController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;
        private HotelsRepositories repository;

        public HotelsController(DBRezervareHotelieraContext context)
        {
            repository = new HotelsRepositories(context);
            _context = context;// this is to be removed
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotels.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = repository.Details(id);               
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            Hotels hotel = new Hotels();
            return View(hotel);
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //ActionResult Create nu merge
        public async Task<IActionResult> Create ([Bind("IdHotel,Stars,HotelName,DescriptionTable")] Hotels hotels)
        {
            if (ModelState.IsValid)
            {

                repository.CreateHotel(hotels);
                return RedirectToAction(nameof(Index));
            }
            return View(hotels);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotels.SingleOrDefaultAsync(m => m.IdHotel == id);
            if (hotels == null)
            {
                return NotFound();
            }
            return View(hotels);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _Edit(/*int id,*/ [Bind("IdHotel,Stars,HotelName,DescriptionTable")] Hotels hotels)
        {
            //if (id != hotels.IdHotel)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelsExists(hotels.IdHotel))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hotels);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotels
                .SingleOrDefaultAsync(m => m.IdHotel == id);
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotels = await _context.Hotels.SingleOrDefaultAsync(m => m.IdHotel == id);
            _context.Hotels.Remove(hotels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelsExists(int id)
        {
            return _context.Hotels.Any(e => e.IdHotel == id);
        }



        // create my http metods


        //[HttpPost]
        //public async Task<IActionResult> SaveHotel ([Bind("Stars,HotelName,DescriptionTable")]Hotels newHotel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(newHotel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(newHotel);
        //}
    }
}
