using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BreweryCollaborationWebbApp.Data;
using BreweryCollaborationWebbApp.Models;

namespace BreweryCollaborationWebbApp.Controllers
{
    public class BeerFanTastesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeerFanTastesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BeerFanTastes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BeerFanTaste.ToListAsync());
        }

        // GET: BeerFanTastes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerFanTaste = await _context.BeerFanTaste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beerFanTaste == null)
            {
                return NotFound();
            }

            return View(beerFanTaste);
        }

        // GET: BeerFanTastes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BeerFanTastes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] BeerFanTaste beerFanTaste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beerFanTaste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beerFanTaste);
        }

        // GET: BeerFanTastes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerFanTaste = await _context.BeerFanTaste.FindAsync(id);
            if (beerFanTaste == null)
            {
                return NotFound();
            }
            return View(beerFanTaste);
        }

        // POST: BeerFanTastes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] BeerFanTaste beerFanTaste)
        {
            if (id != beerFanTaste.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beerFanTaste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerFanTasteExists(beerFanTaste.Id))
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
            return View(beerFanTaste);
        }

        // GET: BeerFanTastes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerFanTaste = await _context.BeerFanTaste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beerFanTaste == null)
            {
                return NotFound();
            }

            return View(beerFanTaste);
        }

        // POST: BeerFanTastes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beerFanTaste = await _context.BeerFanTaste.FindAsync(id);
            _context.BeerFanTaste.Remove(beerFanTaste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerFanTasteExists(int id)
        {
            return _context.BeerFanTaste.Any(e => e.Id == id);
        }
    }
}
