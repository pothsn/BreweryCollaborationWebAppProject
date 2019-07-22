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
                IEnumerable<Brewery> BreweryList = await _context.Brewery.ToListAsync();
                foreach(Brewery brewery in BreweryList)
                {
                    //Query for fans by looking at follows that have the same BreweryId as the one being looked at in that loop
                    brewery.Followers = _context.Fan.Where(fa => (_context.Follow.Where(fo => fo.BreweryId == brewery.Id).Select(fo => fo.FanFollowerId).Contains(fa.Id))).ToList();
                    string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    //Query for logged in brewery
                    var loggedInBrewery = _context.Brewery.Where(i => i.ApplicationId == userId).SingleOrDefault();
                    brewery.LoggedInBreweryId = loggedInBrewery.Id;
                    //Query for each brewery's beers
                    brewery.BreweryBeers = _context.BreweryBeer.Where(b => b.BreweryId == brewery.Id).ToList();
                    
                    //For some reason the Brewery brewery property (part of the FK on BreweryBeer was containing the associated brewery object, causing a bit of a looping issue. Nullifying bandaid applied below.
                    foreach (BreweryBeer bb in brewery.BreweryBeers)
                    {
                        bb.Brewery = null;
                    }

                    //Get matches for logged in brewery
                    IEnumerable<Brewery> breweryMatches = await GetBreweryMatches();

                    //Instantiate view model
                    var viewModel = new BreweriesIndexViewModel();
                    viewModel.Breweries = BreweryList;
                    viewModel.BreweryMatches = breweryMatches;


                    return View(viewModel);
                }
            }
            return View();
        }

        public async Task<IEnumerable<Brewery>> GetBreweryMatches()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //Query for logged in brewery
            var loggedInBrewery = _context.Brewery.Where(i => i.ApplicationId == userId).SingleOrDefault();
            //Query for each brewery's beers
            loggedInBrewery.BreweryBeers = _context.BreweryBeer.Where(b => b.BreweryId == loggedInBrewery.Id).ToList();

            List<Brewery> matchedBreweries = new List<Brewery>();

            //This massive block takes note of the logged in breweries beer styles
            var loggedInBreweryHasAle = false;
            var loggedInBreweryHasLager = false;
            var loggedInBreweryHasIPA = false;
            var loggedInBreweryHasStout = false;
            var loggedInBreweryHasPaleAle = false;
            var loggedInBreweryHasWheatBeer = false;
            var loggedInBreweryHasPilsner = false;
            var loggedInBreweryHasPorter = false;
            var loggedInBreweryHasSour = false;
            var loggedInBreweryHasSaison = false;

            foreach (BreweryBeer beer in loggedInBrewery.BreweryBeers)
            {
                if (beer.StyleId == 1)
                {
                    loggedInBreweryHasAle = true;
                }
                if (beer.StyleId == 2)
                {
                    loggedInBreweryHasLager = true;
                }
                if (beer.StyleId == 3)
                {
                    loggedInBreweryHasIPA = true;
                }
                if (beer.StyleId == 4)
                {
                    loggedInBreweryHasStout = true;
                }
                if (beer.StyleId == 5)
                {
                    loggedInBreweryHasPaleAle = true;
                }
                if (beer.StyleId == 6)
                {
                    loggedInBreweryHasWheatBeer = true;
                }
                if (beer.StyleId == 7)
                {
                    loggedInBreweryHasPilsner = true;
                }
                if (beer.StyleId == 8)
                {
                    loggedInBreweryHasPorter = true;
                }
                if (beer.StyleId == 9)
                {
                    loggedInBreweryHasSour = true;
                }
                if (beer.StyleId == 10)
                {
                    loggedInBreweryHasSaison = true;
                }
            }

            IEnumerable<Brewery> breweries = await _context.Brewery.ToListAsync();
            foreach (Brewery brewery in breweries)
            {
                //Query for fans by looking at follows that have the same BreweryId as the one being looked at in that loop
                brewery.Followers = _context.Fan.Where(fa => (_context.Follow.Where(fo => fo.BreweryId == brewery.Id).Select(fo => fo.FanFollowerId).Contains(fa.Id))).ToList();
                //Query for each brewery's beers
                brewery.BreweryBeers = _context.BreweryBeer.Where(b => b.BreweryId == brewery.Id).ToList();

                //For some reason the Brewery brewery property (part of the FK on BreweryBeer was containing the associated brewery object, causing a bit of a looping issue. Nullifying bandaid applied below.
                foreach (BreweryBeer bb in brewery.BreweryBeers)
                {
                    bb.Brewery = null;
                }
            }



            //Loop through all yes collaboration breweries
            foreach (Brewery brewery in breweries)
            {
                if (brewery.Id != loggedInBrewery.Id)
                {

                    //Add brewery to mathed breweries IF it has at least 3 same beer styles AND has at least 2 shared fans AND fans have at least 2 shared tastes AND breweries are < 15 miles apart
                    var beerStyleMatch = false;

                    //This huge block takes note of breweries[i] beer styles
                    var matchingBreweryHasAle = false;
                    var matchingBreweryHasLager = false;
                    var matchingBreweryHasIPA = false;
                    var matchingBreweryHasStout = false;
                    var matchingBreweryHasPaleAle = false;
                    var matchingBreweryHasWheatBeer = false;
                    var matchingBreweryHasPilsner = false;
                    var matchingBreweryHasPorter = false;
                    var matchingBreweryHasSour = false;
                    var matchingBreweryHasSaison = false;

                    foreach (BreweryBeer beer in brewery.BreweryBeers)
                    {

                        if (beer.StyleId == 1)
                        {
                            matchingBreweryHasAle = true;
                        }
                        if (beer.StyleId == 2)
                        {
                            matchingBreweryHasLager = true;
                        }
                        if (beer.StyleId == 3)
                        {
                            matchingBreweryHasIPA = true;
                        }
                        if (beer.StyleId == 4)
                        {
                            matchingBreweryHasStout = true;
                        }
                        if (beer.StyleId == 5)
                        {
                            matchingBreweryHasPaleAle = true;
                        }
                        if (beer.StyleId == 6)
                        {
                            matchingBreweryHasWheatBeer = true;
                        }
                        if (beer.StyleId == 7)
                        {
                            matchingBreweryHasPilsner = true;
                        }
                        if (beer.StyleId == 8)
                        {
                            matchingBreweryHasPorter = true;
                        }
                        if (beer.StyleId == 9)
                        {
                            matchingBreweryHasSour = true;
                        }
                        if (beer.StyleId == 10)
                        {
                            matchingBreweryHasSaison = true;
                        }

                        //Below we count the number of style matches, a match will need to share 3 beer styles
                        var numberOfStyleMatches = 0;
                        if (loggedInBreweryHasAle == true && matchingBreweryHasAle == true)
                        {
                            numberOfStyleMatches++;
                        }
                        if (loggedInBreweryHasLager == true && matchingBreweryHasLager == true)
                        {
                            numberOfStyleMatches++;
                        }
                        if (loggedInBreweryHasIPA == true && matchingBreweryHasIPA == true)
                        {
                            numberOfStyleMatches++;
                        }
                        if (loggedInBreweryHasStout == true && matchingBreweryHasStout == true)
                        {
                            numberOfStyleMatches++;
                        }
                        if (loggedInBreweryHasPaleAle == true && matchingBreweryHasPaleAle == true)
                        {
                            numberOfStyleMatches++;
                        }
                        if (loggedInBreweryHasWheatBeer == true && matchingBreweryHasWheatBeer == true)
                        {
                            numberOfStyleMatches++;
                        }
                        if (loggedInBreweryHasPilsner == true && matchingBreweryHasPilsner == true)
                        {
                            numberOfStyleMatches++;
                        }
                        if (loggedInBreweryHasPorter == true && matchingBreweryHasPorter == true)
                        {
                            numberOfStyleMatches++;
                        }
                        if (loggedInBreweryHasSour == true && matchingBreweryHasSour == true)
                        {
                            numberOfStyleMatches++;
                        }
                        if (loggedInBreweryHasSaison == true && matchingBreweryHasSaison == true)
                        {
                            numberOfStyleMatches++;
                        }

                        //find number of shared followers
                        var numberOfSharedFollowers = 0;

                        foreach (Fan loggedInBreweryFollower in loggedInBrewery.Followers)
                        {
                            foreach (Fan breweryFollower in brewery.Followers)
                            {

                                if (loggedInBreweryFollower.Id == breweryFollower.Id)
                                {
                                    numberOfSharedFollowers++;
                                }
                            }
                        }

                        //Compare shared followers tastes
                        var sharedTastes = 0;

                        foreach (Fan loggedInBreweryFollower in loggedInBrewery.Followers)
                        {
                            foreach (Fan breweryFollower in brewery.Followers)
                            {

                                if (loggedInBreweryFollower.LikesAle == true && breweryFollower.LikesAle == true)
                                {
                                    sharedTastes++;
                                }
                                if (loggedInBreweryFollower.LikesLager == true && breweryFollower.LikesLager == true)
                                {
                                    sharedTastes++;
                                }
                                if (loggedInBreweryFollower.LikesIPA == true && breweryFollower.LikesIPA == true)
                                {
                                    sharedTastes++;
                                }
                                if (loggedInBreweryFollower.LikesStout == true && breweryFollower.LikesStout == true)
                                {
                                    sharedTastes++;
                                }
                                if (loggedInBreweryFollower.LikesPaleAle == true && breweryFollower.LikesPaleAle == true)
                                {
                                    sharedTastes++;
                                }
                                if (loggedInBreweryFollower.LikesWheatBeer == true && breweryFollower.LikesWheatBeer == true)
                                {
                                    sharedTastes++;
                                }
                                if (loggedInBreweryFollower.LikesPilsner == true && breweryFollower.LikesPilsner == true)
                                {
                                    sharedTastes++;
                                }
                                if (loggedInBreweryFollower.LikesPorter == true && breweryFollower.LikesPorter == true)
                                {
                                    sharedTastes++;
                                }
                                if (loggedInBreweryFollower.LikesSour == true && breweryFollower.LikesSour == true)
                                {
                                    sharedTastes++;
                                }
                                if (loggedInBreweryFollower.LikesSaison == true && breweryFollower.LikesSaison == true)
                                {
                                    sharedTastes++;
                                }

                                //Determine if brewry is close enough geographically





                                //Finally determine if breweries[i] is a match
                                if (numberOfStyleMatches >= 3 && numberOfSharedFollowers >= 2 && sharedTastes >= 5)
                                {
                                    beerStyleMatch = true;
                                }

                                if (beerStyleMatch == true && !matchedBreweries.Contains(brewery))
                                {
                                    matchedBreweries.Add(brewery);
                                }

                            }
                        }
                    }

                }               
            }
            return matchedBreweries;
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
            NewsFeedViewModel beerFanTastes = new NewsFeedViewModel();
            //Needs to display total BeerFanTaste likes
            int countOfAleLovers = _context.Fan.Where(bft => bft.LikesAle == true).Count();
            int countOfLagerLovers = _context.Fan.Where(bft => bft.LikesLager == true).Count();
            int countOfIPALovers = _context.Fan.Where(bft => bft.LikesIPA == true).Count();
            int countOfStoutLovers = _context.Fan.Where(bft => bft.LikesStout == true).Count();
            int countOfPaleAleLovers = _context.Fan.Where(bft => bft.LikesPaleAle == true).Count();
            int countOfWheatLovers = _context.Fan.Where(bft => bft.LikesWheatBeer == true).Count();
            int countOfPilsnerLovers = _context.Fan.Where(bft => bft.LikesPilsner == true).Count();
            int countOfPorterLovers = _context.Fan.Where(bft => bft.LikesPorter == true).Count();
            int countOfSoursLovers = _context.Fan.Where(bft => bft.LikesSour == true).Count();
            int countOfSaisonLovers = _context.Fan.Where(bft => bft.LikesSaison == true).Count();

            //Needs to display total Brewery Followers
            //int countOfBreweryFollowers = _context.Follow.Select(bf => bf.BreweryId).Where(bf => bf.IsFollowed).Count();
            //needs to match BeerFanTaste likes with Brewery Beer Styles
            //Needs to match BeerFanTaste likes with Collaboration Beer Styles
            //needs to match breweries with same beer styles in brewery beers
            return View(newsFeedViewModel);
        }


        public async Task<IActionResult> CountFollowers(NewsFeedViewModel newsFeedViewModel)
        {
            //this is a test method so I don't break anything lol

            //count total followers for each brewery
            //Follow follow = _context.Follow.Where(f => f.Fan.Follows == Brewery).Count();
            //display total count of followers


            return View();
        }


        //public async Task<IActionResult> FollowersCount(NewsFeedViewModel newsFeedViewModel)
        //{

        //    IEnumerable<Brewery> BreweryList = await _context.Brewery.Include(s => s.Followers).ToListAsync();
        //    foreach (Brewery brewery in BreweryList)
        //    {
        //        //Query for fans by looking at follows that have the same BreweryId as the one being looked at in that loop
        //        brewery.Followers = _context.Fan.Where(fa => (_context.Follow.Where(fo => fo.BreweryId == brewery.Id).Select(fo => fo.FanFollowerId).Contains(fa.Id))).ToList();
        //        return View(BreweryList);

        //    }


        //    newsFeedViewModel.Brewery.ApplicationId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    //query for the Brewery object that is currently logged in
        //    //string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    //Brewery loggedInBrewery = _context.Brewery.Where(i => i.ApplicationId == userId).SingleOrDefault();
        //    Follow followers = _context.Follow.Where(f => f.ApplicationId == newsFeedViewModel.Brewery.ApplicationId).SingleOrDefault();

        //    //    //query for a count of followers per each brewery
        //    //int followersCount = _context.Follow.Where(bf => bf.BreweryId == true);



        //    if (followers == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        //get id of brewery whos follows we want to count
        //        //NewsFeedViewModel followBreweryId = _context.Follow.Where(f => f.BreweryId == Brewery.Id).Count();
        //        //NewsFeedViewModel breweryFollowers = _context..Where(f => f.BreweryId == this. ).Count();
        //        //int fansOfLakefront = _context.Follow.Where(f => f.IsFollowed == true).Count();
        //        //int fansOfMobCraft = _context.Follow.Where(f => f.MobCraftBrewery == true).Count();
        //        //int fansOfBrokenBat = _context.Follow.Where(f => f.BrokenBatBrewery == true).Count();

        //        //get count of Beer Fan Taste likes
        //        int countOfAleLovers = _context.BeerFanTaste.Where(bft => bft.Ale == true).Count();
        //        int countOfLagerLovers = _context.BeerFanTaste.Where(bft => bft.Lager == true).Count();
        //        int countOfIndiaPaleAleLovers = _context.BeerFanTaste.Where(bft => bft.IndiaPaleAle == true).Count();
        //        int countOfStoutLovers = _context.BeerFanTaste.Where(bft => bft.Stout == true).Count();
        //        int countOfPaleAleLovers = _context.BeerFanTaste.Where(bft => bft.PaleAle == true).Count();
        //        int countOfWheatBeerLovers = _context.BeerFanTaste.Where(bft => bft.WheatBeer == true).Count();
        //        int countOfPilsnerLovers = _context.BeerFanTaste.Where(bft => bft.Pilsner == true).Count();
        //        int countOfPorterLovers = _context.BeerFanTaste.Where(bft => bft.Porter == true).Count();
        //        int countOfSourLovers = _context.BeerFanTaste.Where(bft => bft.Sour == true).Count();
        //        int countOfSaisonLovers = _context.BeerFanTaste.Where(bft => bft.Saison == true).Count();
        //    }

        //    return View();

        //    //gets followers if the followers are bools
        //    //followers.BreweryId = newsFeedViewModel.Brewery.Id;
        //    //followers.BrokenBatBrewery = newsFeedViewModel.Follow.BrokenBatBrewery;
        //    //followers.LakefrontBrewery = newsFeedViewModel.Follow.LakefrontBrewery;
        //    //followers.MobCraftBrewery = newsFeedViewModel.Follow.MobCraftBrewery;


        //    //Follow followersCount = _context.Follow.Where(f => f. == id).SingleOrDefault();


        //    //    // int countOfAleLovers = _context.BeerFanTaste.Where(bft => bft.Ale == true).Count();
        //    //    return null;




        //    //query for beers that have the matching FK, put them inICollection<BreweryBeer> BreweryBeers


        //}




    }

    }
