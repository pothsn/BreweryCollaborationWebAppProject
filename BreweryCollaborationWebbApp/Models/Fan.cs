using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class Fan
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ICollection<BeerFanTaste> BeerFanTastes { get; set; }
        [Display(Name = "Likes Ales")]
        public bool LikesAle { get; set; }
        [Display(Name = "Likes Lagers")]
        public bool LikesLager { get; set; }
        [Display(Name = "Likes IPAs")]
        public bool LikesIPA { get; set; }
        [Display(Name = "Likes Stouts")]
        public bool LikesStout { get; set; }
        [Display(Name = "Likes Pale Ales")]
        public bool LikesPaleAle { get; set; }
        [Display(Name = "Likes Wheat Beers")]
        public bool LikesWheatBeer { get; set; }
        [Display(Name = "Likes Pilsners")]
        public bool LikesPilsner { get; set; }
        [Display(Name = "Likes Porters")]
        public bool LikesPorter { get; set; }
        [Display(Name = "Likes Sours")]
        public bool LikesSour { get; set; }
        [Display(Name = "Likes Saisons")]
        public bool LikesSaison { get; set; }
        //public FollwedBreweries FollwedBreweries { get; set; }
        public ICollection<Follow> Follows  { get; set; }
    }
}
