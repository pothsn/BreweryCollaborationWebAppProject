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
    public class BreweryBeersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreweryBeersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BreweryBeers
        public async Task<IActionResult> Index()
        {
            return View();
            //return View(await _context.BreweryBeer.ToListAsync());
        }

        // GET: BreweryBeers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryBeer = await _context.BreweryBeer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breweryBeer == null)
            {
                return NotFound();
            }

            return View(breweryBeer);
        }

        // GET: BreweryBeers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BreweryBeers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BreweryBeer breweryBeer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(breweryBeer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(breweryBeer);
        }

        // GET: BreweryBeers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryBeer = await _context.BreweryBeer.FindAsync(id);
            if (breweryBeer == null)
            {
                return NotFound();
            }
            return View(breweryBeer);
        }

        // POST: BreweryBeers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BreweryBeer breweryBeer)
        {
            if (id != breweryBeer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(breweryBeer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreweryBeerExists(breweryBeer.Id))
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
            return View(breweryBeer);
        }

        // GET: BreweryBeers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryBeer = await _context.BreweryBeer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breweryBeer == null)
            {
                return NotFound();
            }

            return View(breweryBeer);
        }

        // POST: BreweryBeers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var breweryBeer = await _context.BreweryBeer.FindAsync(id);
            _context.BreweryBeer.Remove(breweryBeer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreweryBeerExists(int id)
        {
            return _context.BreweryBeer.Any(e => e.Id == id);
        }
    }
}
