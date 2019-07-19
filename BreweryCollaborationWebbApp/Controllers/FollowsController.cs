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
    public class FollowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FollowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Follows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Follow.Include(f => f.ApplicationUser).Include(f => f.brewery);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Follows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _context.Follow
                .Include(f => f.ApplicationUser)
                .Include(f => f.brewery)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // GET: Follows/Create
        public IActionResult Create()
        {
            ViewData["ApplicationId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["BreweryId"] = new SelectList(_context.Brewery, "Id", "Id");
            return View();
        }

        // POST: Follows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BreweryId,ApplicationId")] Follow follow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(follow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_context.ApplicationUser, "Id", "Id", follow.ApplicationId);
            ViewData["BreweryId"] = new SelectList(_context.Brewery, "Id", "Id", follow.BreweryId);
            return View(follow);
        }

        public IActionResult Follow(int id)
        {
            //instatiate a follow
            Follow follow = new Follow();
            //set applicationid as fk
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            follow.ApplicationId = userId;

            //Next five lines were how I was trying to populate a list of followers on a brewery object but it does not persist
            //Fan fanFollowing = _context.Fan.Where(f => f.ApplicationId == userId).FirstOrDefault();
            //Brewery breweryBeingFollowed = _context.Brewery.Where(b => b.Id == id).FirstOrDefault();
            //breweryBeingFollowed.Followers = new List<Fan>();
            //breweryBeingFollowed.Followers.Add(fanFollowing);
            //follow.Fan = fanFollowing;

            //set breweryid as fk
            follow.BreweryId = id;
            //add to table
            _context.Follow.Add(follow);
            //save changes
            _context.SaveChanges();
            //return to same view (redirect to index action)           
            return RedirectToAction("Index", "Breweries");
        }

        // GET: Follows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _context.Follow.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.ApplicationUser, "Id", "Id", follow.ApplicationId);
            ViewData["BreweryId"] = new SelectList(_context.Brewery, "Id", "Id", follow.BreweryId);
            return View(follow);
        }

        // POST: Follows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BreweryId,ApplicationId")] Follow follow)
        {
            if (id != follow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(follow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowExists(follow.Id))
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
            ViewData["ApplicationId"] = new SelectList(_context.ApplicationUser, "Id", "Id", follow.ApplicationId);
            ViewData["BreweryId"] = new SelectList(_context.Brewery, "Id", "Id", follow.BreweryId);
            return View(follow);
        }

        // GET: Follows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var follow = await _context.Follow
                .Include(f => f.ApplicationUser)
                .Include(f => f.brewery)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // POST: Follows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var follow = await _context.Follow.FindAsync(id);
            _context.Follow.Remove(follow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowExists(int id)
        {
            return _context.Follow.Any(e => e.Id == id);
        }
    }
}
