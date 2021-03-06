﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta.Entityes;
using Microsoft.AspNetCore.Http;

namespace Licenta.Controlers
{
    public class CustomersController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;

        public CustomersController(DBRezervareHotelieraContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> DetailsCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.IdCustomer == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult CreateCustomer()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer([Bind("IdCustomer,FirstName,LastName,Username,Password,Email,Phone")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                customers.TypeUser = "Normal User";
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> EditCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers.SingleOrDefaultAsync(m => m.IdCustomer == id);
            if (customers == null)
            {
                return NotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(int id, [Bind("IdCustomer,FirstName,LastName,Username,Password,Email,Phone,TypeUser")] Customers customers)
        {
            if (id != customers.IdCustomer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.IdCustomer))
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
            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> DeleteCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .SingleOrDefaultAsync(m => m.IdCustomer == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationsClient = _context.Reservations.Where(x => x.IdCustomer == id).ToList();
            foreach(var item in reservationsClient)
            {
                _context.Reservations.Remove(item);
            }
            var CreditCardClient = _context.CreditCard.Where(x => x.IdClient == id).ToList();
            foreach (var item in CreditCardClient)
            {
                _context.CreditCard.Remove(item);
            }
            var customers = await _context.Customers.SingleOrDefaultAsync(m => m.IdCustomer == id);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.IdCustomer == id);
        }

        #region login logout

        public ActionResult LogIn()
        {          
            //ViewBag["PreviousPageUrl"] = Request..ToString();
            return View();
        }

        [HttpPost]
        public ActionResult LogIn([Bind("Username,Password")]Customers customerLogin)
        {
            var LoginCustomer = _context.Customers
                .Where(x => x.Username == customerLogin.Username && x.Password == customerLogin.Password)
                .FirstOrDefault();
            if (LoginCustomer != null)
            {
                HttpContext.Session.SetInt32("IdCustomer", LoginCustomer.IdCustomer);
                HttpContext.Session.SetString("Username", LoginCustomer.Username);
                HttpContext.Session.SetString("TypeUser", LoginCustomer.TypeUser);
                return RedirectToAction("Welcome", LoginCustomer);
            }
            else
            {
                ModelState.AddModelError("", "username or password is wrong");
            }
            return View(LoginCustomer);
        }

        public ActionResult Welcome(Customers LoginCustomer)
        {
            if(LoginCustomer.IdCustomer == 0)
            {
                LoginCustomer = _context.Customers.Where(x => x.IdCustomer == HttpContext.Session.GetInt32("IdCustomer")).SingleOrDefault();
            }
            if (HttpContext.Session.GetString("Username") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("Username");
                ViewBag.IdCustomer = HttpContext.Session.GetInt32("IdCustomer");
                return View(LoginCustomer);
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }


        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion

        #region Credit card 
        // the id dosant co for some reason
        public async Task<IActionResult> CreditCardClient(int? idClient)
        {
            var _idClient = HttpContext.Session.GetInt32("IdCustomer");
            var list = await _context.CreditCard.Where(x => x.IdClient == _idClient).ToListAsync();
            return View(list);
        }
        [HttpGet]
        public IActionResult AddCreditCard(int id)
        {
            ViewBag.IdCustomer = HttpContext.Session.GetInt32("IdCustomer");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCreditCard([Bind("CardNumber,CardExpireDate,Cvc,MoneyInTheCard")] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                creditCard.IdClient = HttpContext.Session.GetInt32("IdCustomer");
                _context.CreditCard.Add(creditCard);
                await _context.SaveChangesAsync();
                int id = creditCard.IdClient.Value;
                return RedirectToAction("CreditCardClient", new { id = creditCard.IdClient });
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditCreditCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var creditCard = await _context.CreditCard.SingleOrDefaultAsync(x => x.IdCard == id);
            if (creditCard == null)
            {
                return NotFound();
            }
            return View(creditCard);
        }

        [HttpPost]
        public async Task<IActionResult> EditCreditCard([Bind("IdCard,IdClient,CardNumber,CardExpireDate,Cvc,MoneyInTheCard")] CreditCard creditCard)
        {        
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditCardExists(creditCard.IdCard))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CreditCardClient", new { id = creditCard.IdClient });
            }
            return View(creditCard);
        }

        private bool CreditCardExists(int idCard)
        {
            return _context.CreditCard.Any(e => e.IdCard == idCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletCreditCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var creditCard = await _context.CreditCard.SingleOrDefaultAsync(x => x.IdCard == id);
            _context.Remove(creditCard);
            _context.SaveChanges();
            return RedirectToAction("CreditCardClient", new { idClient = creditCard.IdClient });
        }


        #endregion
    }


}
