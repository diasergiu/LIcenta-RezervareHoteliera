﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta.Entityes;

namespace Licenta.Controlers
{
    public class FacilitiesController : Controller
    {
        private readonly DBRezervareHotelieraContext _context;

        public FacilitiesController(DBRezervareHotelieraContext context)
        {
            _context = context;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facilities.ToListAsync());
        }

        // GET: Facilities/Details/5
        public async Task<IActionResult> DetailsFacilitie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilities = await _context.Facilities
                .SingleOrDefaultAsync(m => m.IdFacilities == id);
            if (facilities == null)
            {
                return NotFound();
            }

            return View(facilities);
        }

        // GET: Facilities/Create
        public IActionResult CreateFacilitie()
        {
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFacilities,FacilitiesName,IsChecked")] Facilities facilities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilities);
        }

        // GET: Facilities/Edit/5
        public async Task<IActionResult> EditFacilitie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilities = await _context.Facilities.SingleOrDefaultAsync(m => m.IdFacilities == id);
            if (facilities == null)
            {
                return NotFound();
            }
            return View(facilities);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFacilitie(int id, [Bind("IdFacilities,FacilitiesName,IsChecked")] Facilities facilities)
        {
            if (id != facilities.IdFacilities)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facilities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilitiesExists(facilities.IdFacilities))
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
            return View(facilities);
        }

        // GET: Facilities/Delete/5
        public async Task<IActionResult> DeleteFacilitie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
  
            var facilities = await _context.Facilities
                .SingleOrDefaultAsync(m => m.IdFacilities == id);
            if (facilities == null)
            {
                return NotFound();
            }

            return View(facilities);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("DeleteFacilitie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faciitie_FacilitiHotel = _context.FacilitiesHotel.Where(x => x.IdFacilities == id).ToList();
            foreach(var item in faciitie_FacilitiHotel)
            { 
            _context.FacilitiesHotel.Remove(item);
            }
            var facilities = await _context.Facilities.SingleOrDefaultAsync(m => m.IdFacilities == id);
            _context.Facilities.Remove(facilities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilitiesExists(int id)
        {
            return _context.Facilities.Any(e => e.IdFacilities == id);
        }
    }
}
