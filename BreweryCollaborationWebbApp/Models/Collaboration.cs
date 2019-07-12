using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class Collaboration
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        [NotMapped]
        public List<BeerStyle> BeerStyles { get; set; }

    }
}
