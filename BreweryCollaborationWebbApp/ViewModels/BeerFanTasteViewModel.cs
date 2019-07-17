using BreweryCollaborationWebbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.ViewModels
{
    public class BeerFanTasteViewModel
    {

        public BeerStyle BeerStyle { get; set; }
        public Fan Fan { get; set; }
        public BeerFanTaste BeerFanTaste { get; set; }
    }
}
