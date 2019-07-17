using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BreweryCollaborationWebbApp.Data;
using BreweryCollaborationWebbApp.Models;
using BreweryCollaborationWebbApp.ViewModels;
using System.Security.Claims;

namespace BreweryCollaborationWebbApp.Controllers
{
    public class CollaborationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollaborationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Collaborations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Collaboration.Include(c => c.BeerStyle).Include(c => c.CollaborationRequest);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Collaborations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration
                .Include(c => c.BeerStyle)
                .Include(c => c.CollaborationRequest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collaboration == null)
            {
                return NotFound();
            }

            return View(collaboration);
        }

        // GET: Collaborations/Create
        //public IActionResult Create()
        //{
        //    ViewData["StyleId"] = new SelectList(_context.BeerStyle, "Id", "Id");
        //    ViewData["CollaborationRequestId"] = new SelectList(_context.CollaborationRequest, "Id", "Id");
        //    List<BeerStyle> li = new List<BeerStyle>();
        //    li = _context.BeerStyle.ToList();
        //    ViewBag.listofitems = li;

        //    ViewModels.CollabBeersViewModel collabBeersViewModel = new ViewModels.CollabBeersViewModel
        //    {
        //        BeerStyles = li
        //    };

        //    return View(collabBeersViewModel);

        //}

        public IActionResult Create()
        {

            List<BeerStyle> li = new List<BeerStyle>();
            li = _context.BeerStyle.ToList();
            ViewBag.listofitems = li;

            ViewModels.CollabBeersViewModel collabBeersViewModel = new ViewModels.CollabBeersViewModel
            {
                BeerStyles = li
            };

            return View(collabBeersViewModel);

        }

        // POST: Collaborations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,StyleId,CollaborationRequestId")] Collaboration collaboration)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(collaboration);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["StyleId"] = new SelectList(_context.BeerStyle, "Id", "Id", collaboration.StyleId);
        //    ViewData["CollaborationRequestId"] = new SelectList(_context.CollaborationRequest, "Id", "Id", collaboration.CollaborationRequestId);
        //    return View(collaboration);
        //}

        // POST: Collaborations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CollabBeersViewModel collabBeersViewModel)
        {
            if (ModelState.IsValid)
            {
                Collaboration newCollabBeer = new Collaboration();


                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Brewery loggedInBrewery = _context.Brewery.Where(i => i.ApplicationId == userId).SingleOrDefault();
                newCollabBeer.CollaborationRequestId = loggedInBrewery.Id;
                newCollabBeer.StyleId = collabBeersViewModel.BeerStyle.Id;
                newCollabBeer.Name = collabBeersViewModel.CollabBeerName;



                _context.Add(newCollabBeer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "breweries");

            }

            return View();
        }

        // GET: Collaborations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration.FindAsync(id);
            if (collaboration == null)
            {
                return NotFound();
            }
            ViewData["StyleId"] = new SelectList(_context.BeerStyle, "Id", "Id", collaboration.StyleId);
            ViewData["CollaborationRequestId"] = new SelectList(_context.CollaborationRequest, "Id", "Id", collaboration.CollaborationRequestId);
            return View(collaboration);
        }

        // POST: Collaborations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StyleId,CollaborationRequestId")] Collaboration collaboration)
        {
            if (id != collaboration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collaboration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaborationExists(collaboration.Id))
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
            ViewData["StyleId"] = new SelectList(_context.BeerStyle, "Id", "Id", collaboration.StyleId);
            ViewData["CollaborationRequestId"] = new SelectList(_context.CollaborationRequest, "Id", "Id", collaboration.CollaborationRequestId);
            return View(collaboration);
        }

        // GET: Collaborations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboration = await _context.Collaboration
                .Include(c => c.BeerStyle)
                .Include(c => c.CollaborationRequest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collaboration == null)
            {
                return NotFound();
            }

            return View(collaboration);
        }

        // POST: Collaborations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collaboration = await _context.Collaboration.FindAsync(id);
            _context.Collaboration.Remove(collaboration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaborationExists(int id)
        {
            return _context.Collaboration.Any(e => e.Id == id);
        }
    }
}
