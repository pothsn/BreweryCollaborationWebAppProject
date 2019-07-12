using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class FanFollow
    {
        [Key]

        public int Id  { get; set; }
        
        [NotMapped]
        public List<Fan> Fans { get; set; }


        [NotMapped]
        public List<Brewery> Breweries { get; set; }




    }
}
