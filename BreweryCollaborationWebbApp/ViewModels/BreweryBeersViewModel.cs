using BreweryCollaborationWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.ViewModels
{
    public class BreweryBeersViewModel
    {

        public BeerStyle BeerStyle { get; set; }
        public IEnumerable<Models.BeerStyle> BeerStyles { get; set; }
        public Brewery Brewery { get; set; }
        public string BeerName { get; set; }
    }
}
