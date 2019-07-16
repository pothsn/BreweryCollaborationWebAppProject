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

        [ForeignKey("Fans")]
        public int FanId { get; set; }
        public Fan Fan { get; set; }


        [ForeignKey("Brewery")]
        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }



    }
}
