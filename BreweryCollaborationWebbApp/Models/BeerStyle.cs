using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class BeerStyle
    {

        [Key]

        //[NotMapped]
        //public List<BreweryBeer> BreweryBeers { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

       
    }
}
