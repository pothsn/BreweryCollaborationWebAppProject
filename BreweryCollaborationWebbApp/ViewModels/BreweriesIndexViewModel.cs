using BreweryCollaborationWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.ViewModels
{
    public class BreweriesIndexViewModel
    {
        public virtual IEnumerable<Brewery> Breweries { get; set; }
        public virtual IEnumerable<Brewery> BreweryMatches { get; set; }

    }
}
