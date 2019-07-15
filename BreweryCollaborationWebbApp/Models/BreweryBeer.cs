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
        public int Id{ get; set; }

        public string Name { get; set; }

        [ForeignKey("BeerStyle")]
        public int StyleId { get; set; }
        public BeerStyle BeerStyle { get; set; }

        [ForeignKey("Brewery")]
        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }
    }
}
