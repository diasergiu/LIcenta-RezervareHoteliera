using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta.Entityes;

namespace Licenta.Controlers
{
    public class RoomsController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;

        public RoomsController(DBRezervareHotelieraContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            var dBRezervareHotelieraContext = _context.Rooms.Include(r => r.IdHotelNavigation);
            return View(await dBRezervareHotelieraContext.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> DetailsRoom(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .Include(r => r.IdHotelNavigation)
                .SingleOrDefaultAsync(m => m.IdRoom == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }

        // GET: Rooms/Create
        public IActionResult CreateRoom()
        {
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "HotelName");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoom([Bind("IdRoom,Beds,Reserved,Bath,IdHotel,PriceRoom,RoomNumber")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "HotelName", rooms.IdHotel);
            return View(rooms);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var rooms = await _context.Rooms.SingleOrDefaultAsync(m => m.IdRoom == id);
            if (rooms == null)
            {
                return NotFound();
            }
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "HotelName", rooms.IdHotel);
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoom(int id, [Bind("IdRoom,Beds,Reserved,Bath,IdHotel,PriceRoom,RoomNumber")] Rooms rooms)
        {

            if (id != rooms.IdRoom)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsExists(rooms.IdRoom))
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
            ViewData["IdHotel"] = new SelectList(_context.Hotels, "IdHotel", "HotelName", rooms.IdHotel);
            return View(rooms);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> DeleteRoom(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .Include(r => r.IdHotelNavigation)
                .SingleOrDefaultAsync(m => m.IdRoom == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("DeleteRoom")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deletReservationsFromRoom = _context.Reservations.Where(x => x.IdRoom == id).ToList();

            foreach (var item in deletReservationsFromRoom)
            {
                _context.Reservations.Remove(item);
            }
            var rooms = await _context.Rooms.SingleOrDefaultAsync(m => m.IdRoom == id);
            _context.Rooms.Remove(rooms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.IdRoom == id);
        }
    }
}
