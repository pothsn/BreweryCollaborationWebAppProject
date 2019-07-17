using BreweryCollaborationWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.ViewModels
{
    public class CollabBeersViewModel   
    {

        public BeerStyle BeerStyle { get; set; }
        public IEnumerable<Models.BeerStyle> BeerStyles { get; set; }
        public CollaborationRequest CollaborationRequest { get; set; }
        public string CollabBeerName { get; set; }
        public string BrewSite { get; set; }
    }
}
