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
    public class FanFollowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FanFollowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FanFollows
        public async Task<IActionResult> Index()
        {
            return View(await _context.FanFollow.ToListAsync());
        }

        // GET: FanFollows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fanFollow = await _context.FanFollow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fanFollow == null)
            {
                return NotFound();
            }

            return View(fanFollow);
        }

        // GET: FanFollows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FanFollows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] FanFollow fanFollow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fanFollow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fanFollow);
        }

        // GET: FanFollows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fanFollow = await _context.FanFollow.FindAsync(id);
            if (fanFollow == null)
            {
                return NotFound();
            }
            return View(fanFollow);
        }

        // POST: FanFollows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] FanFollow fanFollow)
        {
            if (id != fanFollow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fanFollow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FanFollowExists(fanFollow.Id))
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
            return View(fanFollow);
        }

        // GET: FanFollows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fanFollow = await _context.FanFollow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fanFollow == null)
            {
                return NotFound();
            }

            return View(fanFollow);
        }

        // POST: FanFollows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fanFollow = await _context.FanFollow.FindAsync(id);
            _context.FanFollow.Remove(fanFollow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FanFollowExists(int id)
        {
            return _context.FanFollow.Any(e => e.Id == id);
        }
    }
}
