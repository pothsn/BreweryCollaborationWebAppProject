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
    public class CollaborationRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollaborationRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CollaborationRequests
        public async Task<IActionResult> Index()
        {
            return View(await _context.CollaborationRequest.ToListAsync());
        }

        // GET: CollaborationRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CollaborationRequest = await _context.CollaborationRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CollaborationRequest == null)
            {
                return NotFound();
            }

            return View(CollaborationRequest);
        }

        // GET: CollaborationRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollaborationRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] CollaborationRequest CollaborationRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(CollaborationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CollaborationRequest);
        }

        // GET: CollaborationRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CollaborationRequest = await _context.CollaborationRequest.FindAsync(id);
            if (CollaborationRequest == null)
            {
                return NotFound();
            }
            return View(CollaborationRequest);
        }

        // POST: CollaborationRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] CollaborationRequest CollaborationRequest)
        {
            if (id != CollaborationRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CollaborationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaborationRequestExists(CollaborationRequest.Id))
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
            return View(CollaborationRequest);
        }

        // GET: CollaborationRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CollaborationRequest = await _context.CollaborationRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CollaborationRequest == null)
            {
                return NotFound();
            }

            return View(CollaborationRequest);
        }

        // POST: CollaborationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CollaborationRequest = await _context.CollaborationRequest.FindAsync(id);
            _context.CollaborationRequest.Remove(CollaborationRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaborationRequestExists(int id)
        {
            return _context.CollaborationRequest.Any(e => e.Id == id);
        }
    }
}
