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
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BreweryCollaborationWebbApp.ViewModels;

namespace BreweryCollaborationWebbApp.Controllers
{
    public class BreweriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        static readonly HttpClient client = new HttpClient();
        public List<Brewery> breweries = new List<Brewery>();


        public BreweriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Breweries
        public async Task<IActionResult> Index()
        {
            ViewBag.GoogleMapsAPIKey = APIKeys.GoogleMapsAPIKey;
            await _context.Brewery.ToListAsync();
            if (this.User.IsInRole("Fan"))
            {
                return RedirectToAction("IndexForFans", "Breweries");
            }
            else
            {
                ViewBag.GoogleMapsAPIKey = APIKeys.GoogleMapsAPIKey;
                return View(await _context.Brewery.ToListAsync());

            }
        }

        // GET: Breweries index for fans
        public async Task<IActionResult> IndexForFans()
        {
            ViewBag.GoogleMapsAPIKey = APIKeys.GoogleMapsAPIKey;
            return View(await _context.Brewery.ToListAsync());
        }



        // GET: Breweries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
        if (id == null)
            {
                return NotFound();
            }
            var brewery = await _context.Brewery.FirstOrDefaultAsync(b => b.Id == id);
            brewery.BreweryBeers = _context.BreweryBeer.Where(b => b.BreweryId == brewery.Id).ToList();

            if (brewery == null)
            {
                return NotFound();
            }
            return View(brewery);
        }


        // GET: BreweryUserDetails
        public async Task<IActionResult> UserDetails()
        {
            //get application user's Id
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Brewery loggedInBrewery =  _context.Brewery.Where(i => i.ApplicationId == userId).SingleOrDefault();
            //query for beers that have the matching FK, put them inICollection<BreweryBeer> BreweryBeers
            loggedInBrewery.BreweryBeers = _context.BreweryBeer.Where(b => b.BreweryId == loggedInBrewery.Id).ToList();

            if (loggedInBrewery == null)
            {
                return NotFound();
            }

            return View(loggedInBrewery);
        }

        // GET: Breweries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Breweries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brewery brewery)
        {
            if (ModelState.IsValid)
            {              
                // assign ApplicationId a la TrashCollector
                brewery.ApplicationId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _context.Add(brewery);
                await Geocode(brewery);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(brewery);
        }


        static async Task Geocode(Brewery brewery)
        {
            string breweryURL = ("https://maps.googleapis.com/maps/api/geocode/json?address=" + brewery.Address + brewery.City + brewery.State + APIKeys.GoogleGeocodingAPI);
            try
            {
                HttpResponseMessage response = await client.GetAsync(breweryURL);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var thisResult = JsonConvert.DeserializeObject<JObject>(responseBody);
                var latitude = thisResult["results"][0]["geometry"]["location"]["lat"];
                brewery.Latitude = latitude.ToObject<double>();
                var longitude = thisResult["results"][0]["geometry"]["location"]["lng"];
                brewery.Longitude = longitude.ToObject<double>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        // GET: Breweries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Brewery.FindAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }
            return View(brewery);
        }

        // POST: Breweries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,City,State,Zipcode,Website,Collaboration,ApplicationId,Latitude,Longitude,Image")] Brewery brewery)
        {
            if (id != brewery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brewery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreweryExists(brewery.Id))
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
            return View(brewery);
        }

        // GET: Breweries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Brewery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery);
        }

        // POST: Breweries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brewery = await _context.Brewery.FindAsync(id);
            _context.Brewery.Remove(brewery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreweryExists(int id)
        {
            return _context.Brewery.Any(e => e.Id == id);
        }
    }
}
