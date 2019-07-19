using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BreweryCollaborationWebbApp.Data;
using BreweryCollaborationWebbApp.Models;
using System.Security.Claims;

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
            var applicationDbContext = _context.BeerFanTaste.Include(b => b.BeerStyle).Include(b => b.Fan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BeerFanTastes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beerFanTaste = await _context.BeerFanTaste
                .Include(b => b.BeerStyle)
                .Include(b => b.Fan)
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
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Fan fan = _context.Fan.Where(f => f.ApplicationId == userId).SingleOrDefault();
            ViewData["BeerStyleId"] = new SelectList(_context.BeerStyle, "Id", "Id");
            ViewData["FanId"] = new SelectList(_context.Fan, "Id", "Id");
            return View();
        }

        // POST: BeerFanTastes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeerFanTaste beerFanTaste)
        {
            //gets here after Fan user creates a profile. 
            //Needs to create an object of the BeerFanTaste with the FanId as the foreign key
            //
            BeerFanTaste fanTaste = new BeerFanTaste();
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Fan fan = _context.Fan.Where(f => f.ApplicationId == userId).SingleOrDefault();

            fanTaste.Fan.ApplicationId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (ModelState.IsValid)
            {

                _context.Add(fanTaste);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexForFans", "Breweries");
            }
            ViewData["BeerStyleId"] = new SelectList(_context.BeerStyle, "Id", "Id", beerFanTaste.BeerStyleId);
            ViewData["FanId"] = new SelectList(_context.Fan, "Id", "Id", beerFanTaste.FanId);
            
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
            ViewData["BeerStyleId"] = new SelectList(_context.BeerStyle, "Id", "Id", beerFanTaste.BeerStyleId);
            ViewData["FanId"] = new SelectList(_context.Fan, "Id", "Id", beerFanTaste.FanId);
            return View(beerFanTaste);
        }

        // POST: BeerFanTastes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FanId,BeerStyleId")] BeerFanTaste beerFanTaste)
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
            ViewData["BeerStyleId"] = new SelectList(_context.BeerStyle, "Id", "Id", beerFanTaste.BeerStyleId);
            ViewData["FanId"] = new SelectList(_context.Fan, "Id", "Id", beerFanTaste.FanId);
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
                .Include(b => b.BeerStyle)
                .Include(b => b.Fan)
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
