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
        public int Id { get; set; }


        public string Name { get; set; }
        public virtual ICollection<BreweryBeer> BreweryBeers { get; set; }



    }
}
