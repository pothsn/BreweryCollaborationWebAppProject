using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class BreweryBeer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int StylesId { get; set; }
        //public BeerStyle BeerStyle { get; set; }
        [NotMapped]
        public List<BeerStyle> BeerStyles { get; set; }

        public int BreweryId { get; set; }
        //public Brewery Brewery { get; set; }
        [NotMapped]
        public List<Brewery> Breweries { get; set; }


    }
}
