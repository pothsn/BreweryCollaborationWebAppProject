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
    public class BreweryFollowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreweryFollowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BreweryFollows
        public async Task<IActionResult> Index()
        {
            return View(await _context.BreweryFollow.ToListAsync());
        }

        // GET: BreweryFollows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryFollow = await _context.BreweryFollow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breweryFollow == null)
            {
                return NotFound();
            }

            return View(breweryFollow);
        }

        // GET: BreweryFollows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BreweryFollows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] BreweryFollow breweryFollow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(breweryFollow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(breweryFollow);
        }

        // GET: BreweryFollows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryFollow = await _context.BreweryFollow.FindAsync(id);
            if (breweryFollow == null)
            {
                return NotFound();
            }
            return View(breweryFollow);
        }

        // POST: BreweryFollows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] BreweryFollow breweryFollow)
        {
            if (id != breweryFollow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(breweryFollow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreweryFollowExists(breweryFollow.Id))
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
            return View(breweryFollow);
        }

        // GET: BreweryFollows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryFollow = await _context.BreweryFollow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breweryFollow == null)
            {
                return NotFound();
            }

            return View(breweryFollow);
        }

        // POST: BreweryFollows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var breweryFollow = await _context.BreweryFollow.FindAsync(id);
            _context.BreweryFollow.Remove(breweryFollow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreweryFollowExists(int id)
        {
            return _context.BreweryFollow.Any(e => e.Id == id);
        }
    }
}
