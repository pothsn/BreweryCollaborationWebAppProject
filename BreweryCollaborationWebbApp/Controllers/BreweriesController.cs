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
                IEnumerable<Brewery> BreweryList = await _context.Brewery.Include(s=>s.Followers).ToListAsync();
                foreach(Brewery brewery in BreweryList)
                {
                  //brewery.Followers = _context.Fan.Where(s=> s.Id== brewery).ToListAsync();
                }
                return View(BreweryList);

            }
        }

        // GET: Breweries index for fans
        public async Task<IActionResult> IndexForFans()
        {
            ViewBag.GoogleMapsAPIKey = APIKeys.GoogleMapsAPIKey;
            //This lambda looks through all breweries and adds ones that share an id with a SenderId OR a ReceiverId located on any CollaborationRequest object
            IEnumerable<Brewery> breweriesWithCollaborations = _context.Brewery.Where(b => (_context.CollaborationRequest.Where(c => c.SenderId == b.Id).Select(c => c.SenderId).Contains(b.Id)) || (_context.CollaborationRequest.Where(c => c.ReceiverId == b.Id).Select(c => c.ReceiverId).Contains(b.Id))).ToList();
            return View(breweriesWithCollaborations);
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

        // Add fan to brewery followers collection
        //public async Task <IActionResult> AddFollowerToBreweryFollowers(Fan fan)
        //{

        //}

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,City,State,Zipcode,Email,Website,Collaboration,ApplicationId,Latitude,Longitude,Image,CollaborationRequests,Followers")] Brewery brewery)
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

        public async Task<IActionResult> DataAnalysis(NewsFeedViewModel newsFeedViewModel)
        {
            return null;
        }

        public async Task<IActionResult> FollowersCount(NewsFeedViewModel newsFeedViewModel)
        {
            newsFeedViewModel.Brewery.ApplicationId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //query for the Brewery object that is currently logged in
            //string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //Brewery loggedInBrewery = _context.Brewery.Where(i => i.ApplicationId == userId).SingleOrDefault();
            Follow followers = _context.Follow.Where(f => f.ApplicationId == newsFeedViewModel.Brewery.ApplicationId).SingleOrDefault();

            if (followers == null)
            {
                return NotFound();
            }
            else
            {
                //get id of brewery whos follows we want to count
                //NewsFeedViewModel followBreweryId = _context.Follow.Where(f => f.BreweryId == Brewery.Id).Count();
                //NewsFeedViewModel breweryFollowers = _context..Where(f => f.BreweryId == this. ).Count();
                //int fansOfLakefront = _context.Follow.Where(f => f.IsFollowed == true).Count();
                //int fansOfMobCraft = _context.Follow.Where(f => f.MobCraftBrewery == true).Count();
                //int fansOfBrokenBat = _context.Follow.Where(f => f.BrokenBatBrewery == true).Count();

                //get count of Beer Fan Taste likes
                int countOfAleLovers = _context.BeerFanTaste.Where(bft => bft.Ale == true).Count();
                int countOfLagerLovers = _context.BeerFanTaste.Where(bft => bft.Lager == true).Count();
                int countOfIndiaPaleAleLovers = _context.BeerFanTaste.Where(bft => bft.IndiaPaleAle == true).Count();
                int countOfStoutLovers = _context.BeerFanTaste.Where(bft => bft.Stout == true).Count();
                int countOfPaleAleLovers = _context.BeerFanTaste.Where(bft => bft.PaleAle == true).Count();
                int countOfWheatBeerLovers = _context.BeerFanTaste.Where(bft => bft.WheatBeer == true).Count();
                int countOfPilsnerLovers = _context.BeerFanTaste.Where(bft => bft.Pilsner == true).Count();
                int countOfPorterLovers = _context.BeerFanTaste.Where(bft => bft.Porter == true).Count();
                int countOfSourLovers = _context.BeerFanTaste.Where(bft => bft.Sour == true).Count();
                int countOfSaisonLovers = _context.BeerFanTaste.Where(bft => bft.Saison == true).Count();
            }

            return View();

            //gets followers if the followers are bools
            //followers.BreweryId = newsFeedViewModel.Brewery.Id;
            //followers.BrokenBatBrewery = newsFeedViewModel.Follow.BrokenBatBrewery;
            //followers.LakefrontBrewery = newsFeedViewModel.Follow.LakefrontBrewery;
            //followers.MobCraftBrewery = newsFeedViewModel.Follow.MobCraftBrewery;


            //Follow followersCount = _context.Follow.Where(f => f. == id).SingleOrDefault();

            //    //query for a list of followers per each brewery
            //    int followersCount = _context.Follow.Where(bf => bf. == true);
            //    // int countOfAleLovers = _context.BeerFanTaste.Where(bft => bft.Ale == true).Count();
            //    return null;




            //query for beers that have the matching FK, put them inICollection<BreweryBeer> BreweryBeers


        }




        }

    }
