using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BreweryCollaborationWebbApp.Models;
using BreweryCollaborationWebbApp.ViewModels;

namespace BreweryCollaborationWebbApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult NewsFeedView()
        {
            return View();
        }

        public  IActionResult NewsFeedViewTest(NewsFeedViewModel newsFeedViewModel)
        {
            //var timeStamp = new DateTime();
            //timeStamp = newsFeedViewModel.Collaborations.Where(t => t.WhenCreated == DateTime.Today);
     
            //var thing = new NewsFeedViewModel();
            //thing = newsFeedViewModel.Collaboration.Collaborations.Where(x => x.WhenCreated == DateTime.Now); 
            ////thing = newsFeedViewModel.Collaborations.Where(x => x.WhenCreated == DateTime.Now);

            //thing = newsFeedViewModel.Collaborations.Where(x => x.WhenCreated == DateTime.Now);

            //return View(thing);

            
            return View();
 
        }
    }
}
