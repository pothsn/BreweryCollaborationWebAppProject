using Microsoft.AspNetCore.Mvc;
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

        //[BindProperty]
        //public List<BeerFanTaste> BeerPreferences { get; set; } = new List<BeerFanTaste>();


        public bool Ale { get; set; }
        public bool Lager { get; set; }
        public bool IndiaPaleAle { get; set; }
        public bool Stout { get; set; }
        public bool PaleAle { get; set; }
        public bool WheatBeer { get; set; }
        public bool Pilsner { get; set; }
        public bool Porter { get; set; }
        public bool Sour { get; set; }
        public bool Saison { get; set; }




    }
}
