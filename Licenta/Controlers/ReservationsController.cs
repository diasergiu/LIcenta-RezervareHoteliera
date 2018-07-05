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
        public async Task<IActionResult> Index(int? id)
        {
            var dBRezervareHotelieraContext = _context.Reservations.Include(r => r.IdCustomerNavigation).Include(r => r.IdRoomNavigation).Include(x => x.IdRoomNavigation.IdHotelNavigation).Include(x => x.IdRoomNavigation.IdHotelNavigation.IdLocationNavigation).Where(x => x.IdCustomer == id);
            return View(await dBRezervareHotelieraContext.ToListAsync());

        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> DetailsReservation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await _context.Reservations
                .Include(r => r.IdCustomerNavigation)
                .Include(r => r.IdRoomNavigation)
                .Include(x => x.IdRoomNavigation.IdHotelNavigation)
                .Include(x => x.IdRoomNavigation.IdHotelNavigation.IdLocationNavigation)
                .SingleOrDefaultAsync(m => m.IdReservations == id);
            if (reservations == null)
            {
                return NotFound();
            }            
            return View(reservations);
        }

        // GET: Reservations/Create
        public IActionResult CreateReservation()
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
        public async Task<IActionResult> CreateReservation([Bind("IdReservations,CheckIn,CheckOut,IdRoom,IdCustomer")] Reservations reservations)
        {

            if (ModelState.IsValid)
            {
                var rooms = _context.Rooms.Where(x => x.IdRoom == reservations.IdRoom).FirstOrDefault();

                var reservationsList = _context.Reservations.Where(x => x.IdRoom == rooms.IdRoom)
                    .OrderBy(x => x.CheckOut).ToList();
                bool found = reservations.CheckIn < reservationsList[0].CheckIn
                    || reservations.CheckOut > reservationsList[reservationsList.Count() - 1].CheckOut;

                for (int i = 0; i < reservationsList.Count() - 2; ++i)
                {

                    if (reservationsList[i].CheckOut < reservations.CheckIn && reservationsList[i + 1].CheckIn > reservations.CheckOut)
                    {
                        found = true;                       
                        break;
                    }
                    if (found)
                    {
                        _context.Add(reservations);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                }

                ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", reservations.IdCustomer);
                ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom", reservations.IdRoom);
                return View(reservations);


            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", reservations.IdCustomer);
            ViewData["IdRoom"] = new SelectList(_context.Rooms, "IdRoom", "IdRoom", reservations.IdRoom);
            return View(reservations);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> EditReservation(int? id)
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
        public async Task<IActionResult> EditReservation(int id, [Bind("IdReservations,CheckIn,CheckOut,IdRoom,IdCustomer")] Reservations reservations)
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
        public async Task<IActionResult> DeleteReservation(int? id)
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
        [HttpPost, ActionName("DeleteReservation")]
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

        public async Task<IActionResult> CreateReservation(DateTime CheckIn, DateTime CheckOut, int IdRoom, int IdUser,int IdCard)
        {

            if (ModelState != null)
            {
                if(IdUser != 0)
                { 
                int roomPrice = (from r in _context.Rooms where r.IdRoom == IdRoom select r.PriceRoom).First().Value;
                    //var creditCard = _context.CreditCard.Where(x => x.IdCard == IdCard).SingleOrDefault();
                    //if(creditCard.MoneyInTheCard < roomPrice)
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    creditCard.MoneyInTheCard -= roomPrice;
                    //    _context.CreditCard.Update(creditCard);
                    //    _context.SaveChanges();
                    //}
                Reservations newReservation = new Reservations();
                newReservation.CheckIn = CheckIn;
                newReservation.CheckOut = CheckOut;
                newReservation.IdRoom = IdRoom;
                newReservation.IdCustomer = IdUser;
               
                        
                _context.Add(newReservation);
                await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction("LogIn", "Customers");
                }
            }

            return Redirect("/Home");
        }
    }
}
