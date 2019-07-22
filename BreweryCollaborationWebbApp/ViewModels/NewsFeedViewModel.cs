using BreweryCollaborationWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.ViewModels
{
    public class NewsFeedViewModel
    {
        public Brewery Brewery { get; set; }
        public IEnumerable<Models.Brewery> Breweries { get; set; }
        public BeerStyle BeerStyle { get; set; }
        public IEnumerable<Models.BeerStyle> BeerStyles { get; set; }
        public BreweryBeer BreweryBeer { get; set; }
        public IEnumerable<BreweryBeer> BreweryBeers { get; set; }
        public BeerFanTaste BeerFanTaste { get; set; }
        public IEnumerable<BeerFanTaste> BeerFanTastes { get; set; }
        public Collaboration Collaboration { get; set; }
        public IEnumerable<Collaboration> Collaborations { get; set; }
        public CollaborationRequest CollaborationRequest { get; set; }
        public IEnumerable<CollaborationRequest> CollaborationRequests { get; set; }
        public  Fan Fan { get; set; }
        public Follow Follow { get; set; }
        
    }
}
