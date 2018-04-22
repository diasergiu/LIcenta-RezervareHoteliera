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
    public class ReservationsController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;

        public ReservationsController(DBRezervareHotelieraContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var dBRezervareHotelieraContext = _context.Reservations.Include(r => r.IdCustomerNavigation).Include(r => r.IdRoomNavigation);
            return View(await dBRezervareHotelieraContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations
                .Include(r => r.IdCustomerNavigation)
                .Include(r => r.IdRoomNavigation)
                .SingleOrDefaultAsync(m => m.IdReservations == id);
            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer");
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReservations,CheckIn,CheckOut,IdRoom,IdCustomer")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", reservations.IdCustomer);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom", reservations.IdRoom);
            return View(reservations);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations.SingleOrDefaultAsync(m => m.IdReservations == id);
            if (reservations == null)
            {
                return NotFound();
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", reservations.IdCustomer);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom", reservations.IdRoom);
            return View(reservations);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReservations,CheckIn,CheckOut,IdRoom,IdCustomer")] Reservations reservations)
        {
            if (id != reservations.IdReservations)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationsExists(reservations.IdReservations))
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
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", reservations.IdCustomer);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom", reservations.IdRoom);
            return View(reservations);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations
                .Include(r => r.IdCustomerNavigation)
                .Include(r => r.IdRoomNavigation)
                .SingleOrDefaultAsync(m => m.IdReservations == id);
            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservations = await _context.Reservations.SingleOrDefaultAsync(m => m.IdReservations == id);
            _context.Reservations.Remove(reservations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationsExists(int id)
        {
            return _context.Reservations.Any(e => e.IdReservations == id);
        }
    }
}
