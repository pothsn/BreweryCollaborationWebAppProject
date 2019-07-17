﻿using System;
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
            var applicationDbContext = _context.CollaborationRequest.Include(c => c.ApplicationUser).Include(c => c.Brewery);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CollaborationRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborationRequest = await _context.CollaborationRequest
                .Include(c => c.ApplicationUser)
                .Include(c => c.Brewery)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collaborationRequest == null)
            {
                return NotFound();
            }

            return View(collaborationRequest);
        }

        // GET: CollaborationRequests/Create
        public IActionResult Create()
        {
            ViewData["ApplicationId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["BreweryId"] = new SelectList(_context.Brewery, "Id", "Id");
            return View();
        }

        // POST: CollaborationRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationId,BreweryId")] CollaborationRequest collaborationRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collaborationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_context.ApplicationUser, "Id", "Id", collaborationRequest.ApplicationId);
            ViewData["BreweryId"] = new SelectList(_context.Brewery, "Id", "Id", collaborationRequest.BreweryId);
            return View(collaborationRequest);
        }

        // GET: CollaborationRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborationRequest = await _context.CollaborationRequest.FindAsync(id);
            if (collaborationRequest == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.ApplicationUser, "Id", "Id", collaborationRequest.ApplicationId);
            ViewData["BreweryId"] = new SelectList(_context.Brewery, "Id", "Id", collaborationRequest.BreweryId);
            return View(collaborationRequest);
        }

        // POST: CollaborationRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationId,BreweryId")] CollaborationRequest collaborationRequest)
        {
            if (id != collaborationRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collaborationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaborationRequestExists(collaborationRequest.Id))
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
            ViewData["ApplicationId"] = new SelectList(_context.ApplicationUser, "Id", "Id", collaborationRequest.ApplicationId);
            ViewData["BreweryId"] = new SelectList(_context.Brewery, "Id", "Id", collaborationRequest.BreweryId);
            return View(collaborationRequest);
        }

        // GET: CollaborationRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborationRequest = await _context.CollaborationRequest
                .Include(c => c.ApplicationUser)
                .Include(c => c.Brewery)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collaborationRequest == null)
            {
                return NotFound();
            }

            return View(collaborationRequest);
        }

        // POST: CollaborationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collaborationRequest = await _context.CollaborationRequest.FindAsync(id);
            _context.CollaborationRequest.Remove(collaborationRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaborationRequestExists(int id)
        {
            return _context.CollaborationRequest.Any(e => e.Id == id);
        }
    }
}