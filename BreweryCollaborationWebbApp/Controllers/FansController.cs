using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BreweryCollaborationWebbApp.Data;
using BreweryCollaborationWebbApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BreweryCollaborationWebbApp.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BreweryCollaborationWebbApp.Controllers
{
    public class FansController : Controller
    {
        private readonly ApplicationDbContext _context;
        static readonly HttpClient client = new HttpClient();

        public FansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fans
        public async Task<IActionResult> Index()
        {

            return View(await _context.Fan.ToListAsync());
            //ToListAsync()); 
        }

        public async Task<IActionResult> BreweriesIndex()
        {

            return View(await _context.Brewery.ToListAsync());
            //ToListAsync()); ;
        }

        // GET: Fans/Details/5
        public async Task<IActionResult> BreweriesDetails(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var fan = await _context.Brewery
                .FirstOrDefaultAsync(m => m.Id == id);
            //var breweryBeers = await _context.BreweryBeer.FirstOrDefaultAsync(b => b.Id == id);

            if (fan == null)
            {
                return NotFound();
            }

            return View(fan);
        }


        // GET: Breweries/Details/5
        public async Task<IActionResult> UserDetails()
        {
            //get application user's Id
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Fan fan = _context.Fan.Where(f => f.ApplicationId == userId).SingleOrDefault();


            if (fan == null)
            {
                return NotFound();
            }

            return View(fan);
        }

        // GET: Fans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,City,State,Zipcode,ApplicationId,ApplicationUser,Latitude,Longitude,LikesAle,LikesLager,LikesIPA,LikesStout,LikesPaleAle,LikesWheatBeer,LikesPilsner,LikesPorter,LikesSour,LikesSaison")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                fan.ApplicationId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _context.Add(fan);
                await Geocode(fan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fan);
        }

        static async Task Geocode(Fan fan)
        {
            string fanURL = ("https://maps.googleapis.com/maps/api/geocode/json?address=" + fan.Address + fan.City + fan.State + APIKeys.GoogleGeocodingAPI);
            try
            {
                HttpResponseMessage response = await client.GetAsync(fanURL);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var thisResult = JsonConvert.DeserializeObject<JObject>(responseBody);
                var latitude = thisResult["results"][0]["geometry"]["location"]["lat"];
                fan.Latitude = latitude.ToObject<double>();
                var longitude = thisResult["results"][0]["geometry"]["location"]["lng"];
                fan.Longitude = longitude.ToObject<double>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        // GET: Fans/Edit/5
        public async Task<IActionResult> Edit(int? id )
        {
            if (id == null)
            {
                return NotFound();
            }

            var fan = await _context.Fan.FindAsync(id);
            
            if (fan == null)
            {
                return NotFound();
            }
            return View(fan);

        }

        // POST: Fans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        // POST: Fans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,City,State,Zipcode,ApplicationId,ApplicationUser,Latitude,Longitude,LikesAle,LikesLager,LikesIPA,LikesStout,LikesPaleAle,LikesWheatBeer,LikesPilsner,LikesPorter,LikesSour,LikesSaison")] Fan fan)
        {
            if (id != fan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FanExists(fan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UserDetails));
            }
            return View(fan);
        }

        // POST: Fans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BeerTastes(int id, BeerFanTasteViewModel beerFanTasteViewModel)
        {
            //query for the BeerFanTaste object that has a FanId of int id
            BeerFanTaste fanUpdate = _context.BeerFanTaste.Where(f => f.FanId == id).SingleOrDefault();

            //need the "OrDefault" thinger at the end of the query
            //if fanUpdate == null then create a new instance of the thingy
            if (fanUpdate == null)
            {
                fanUpdate = new BeerFanTaste();
                fanUpdate.BeerStyle = beerFanTasteViewModel.BeerStyle;
                fanUpdate.FanId.ToString();
                fanUpdate.FanId = beerFanTasteViewModel.Fan.Id;
                fanUpdate.Ale = beerFanTasteViewModel.BeerFanTaste.Ale;
                fanUpdate.Lager = beerFanTasteViewModel.BeerFanTaste.Lager;
                fanUpdate.IndiaPaleAle = beerFanTasteViewModel.BeerFanTaste.IndiaPaleAle;
                fanUpdate.Stout = beerFanTasteViewModel.BeerFanTaste.Stout;
                fanUpdate.PaleAle = beerFanTasteViewModel.BeerFanTaste.PaleAle;
                fanUpdate.WheatBeer = beerFanTasteViewModel.BeerFanTaste.WheatBeer;
                fanUpdate.Pilsner = beerFanTasteViewModel.BeerFanTaste.Pilsner;
                fanUpdate.Porter = beerFanTasteViewModel.BeerFanTaste.Porter;
                fanUpdate.Sour = beerFanTasteViewModel.BeerFanTaste.Sour;
                fanUpdate.Saison = beerFanTasteViewModel.BeerFanTaste.Saison;
                _context.Add(fanUpdate);
                await _context.SaveChangesAsync();


                //fanUpdate = new BeerFanTaste();
                //fanUpdate = new BeerFanTaste();
                //create new instance and give it values as seen below
                //If fanUpdate==null is reached, then _context.Add(derp);
            }
            else
            {
                fanUpdate.BeerStyle = beerFanTasteViewModel.BeerStyle;
                fanUpdate.Ale = beerFanTasteViewModel.BeerFanTaste.Ale;
                fanUpdate.Lager = beerFanTasteViewModel.BeerFanTaste.Lager;
                fanUpdate.IndiaPaleAle = beerFanTasteViewModel.BeerFanTaste.IndiaPaleAle;
                fanUpdate.Stout = beerFanTasteViewModel.BeerFanTaste.Stout;
                fanUpdate.PaleAle = beerFanTasteViewModel.BeerFanTaste.PaleAle;
                fanUpdate.WheatBeer = beerFanTasteViewModel.BeerFanTaste.WheatBeer;
                fanUpdate.Pilsner = beerFanTasteViewModel.BeerFanTaste.Pilsner;
                fanUpdate.Porter = beerFanTasteViewModel.BeerFanTaste.Porter;
                fanUpdate.Sour = beerFanTasteViewModel.BeerFanTaste.Sour;
                fanUpdate.Saison = beerFanTasteViewModel.BeerFanTaste.Saison;

                _context.Update(beerFanTasteViewModel);
                await _context.SaveChangesAsync();
                return View(beerFanTasteViewModel);
                //return RedirectToAction("BeerTastes", "Fans");

            }
            return View(beerFanTasteViewModel);
            //return View("UserDetails", "Fans");
       

            // int countOfAleLovers = _context.BeerFanTaste.Where(bft => bft.Ale == true).Count();

        }

        // GET: Fans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fan = await _context.Fan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fan == null)
            {
                return NotFound();
            }

            return View(fan);
        }

        // POST: Fans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fan = await _context.Fan.FindAsync(id);
            _context.Fan.Remove(fan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FanExists(int id)
        {
            return _context.Fan.Any(e => e.Id == id);
        }
    }
}
