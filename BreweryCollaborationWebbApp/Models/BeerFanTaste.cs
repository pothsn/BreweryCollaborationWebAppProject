using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class BeerFanTaste
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Fan")]
        public int FanId { get; set; }
        public Fan Fan { get; set; }

        [ForeignKey("BeerStyle")]
        public int BeerStyleId { get; set; }
        public BeerStyle BeerStyle { get; set; }

    }
}
