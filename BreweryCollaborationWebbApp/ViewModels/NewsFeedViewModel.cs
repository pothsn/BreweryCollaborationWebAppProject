using BreweryCollaborationWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.ViewModels
{
    public class NewsFeedViewModel
    {
        public BeerStyle BeerStyle { get; set; }
        public Brewery Brewery { get; set; }
        public BreweryBeer BreweryBeer { get; set; }
        public Collaboration Collaboration { get; set; }

        public CollaborationRequest CollaborationRequest { get; set; }



    }
}
