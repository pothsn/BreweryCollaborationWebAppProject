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


        //public virtual BeerStyle BeerStyle { get; set; }
        //public virtual Brewery Brewery { get; set; }

        //[Key]
        //[Column(Order =1)]
        //public int BreweryId { get; set; }
        //[Key]
        //[Column(Order =2)]
        //public int BeerStyleId { get; set; }

        //public virtual BeerStyle BeerStyle { get; set; }
        //public virtual Brewery Brewery { get; set; }


        //[Display(Name = "BeerStyle")]
        //public int StyleId { get; set; }


        //[ForeignKey("StylesId")]
        //public BeerStyle BeerStyle { get; set; }
        //public int StylesId { get; set; }

        //[NotMapped]
        //public List<BeerStyle> BeerStyles { get; set; }

        //[Display(Name = "Brewery")]
        //public int BreweryId { get; set; }




        //[NotMapped]
        //public List<Brewery> Breweries { get; set; }


    }
}
