using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class BreweryFollow
    {

        [Key]
        public  int Id { get; set; }

        [NotMapped]
        public List<BreweryFollow> BreweryFollows { get; set; }
        [NotMapped]
        public List<FanFollow> FanFollows { get; set; }



    }
}
