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
    public class CollaborationJunctionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollaborationJunctionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CollaborationJunctions
        public async Task<IActionResult> Index()
        {
            return View(await _context.CollaborationJunction.ToListAsync());
        }

        // GET: CollaborationJunctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CollaborationJunction = await _context.CollaborationJunction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CollaborationJunction == null)
            {
                return NotFound();
            }

            return View(CollaborationJunction);
        }

        // GET: CollaborationJunctions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollaborationJunctions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] CollaborationJunction CollaborationJunction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(CollaborationJunction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CollaborationJunction);
        }

        // GET: CollaborationJunctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CollaborationJunction = await _context.CollaborationJunction.FindAsync(id);
            if (CollaborationJunction == null)
            {
                return NotFound();
            }
            return View(CollaborationJunction);
        }

        // POST: CollaborationJunctions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] CollaborationJunction CollaborationJunction)
        {
            if (id != CollaborationJunction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CollaborationJunction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaborationJunctionExists(CollaborationJunction.Id))
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
            return View(CollaborationJunction);
        }

        // GET: CollaborationJunctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CollaborationJunction = await _context.CollaborationJunction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CollaborationJunction == null)
            {
                return NotFound();
            }

            return View(CollaborationJunction);
        }

        // POST: CollaborationJunctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CollaborationJunction = await _context.CollaborationJunction.FindAsync(id);
            _context.CollaborationJunction.Remove(CollaborationJunction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaborationJunctionExists(int id)
        {
            return _context.CollaborationJunction.Any(e => e.Id == id);
        }
    }
}
