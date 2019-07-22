using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class Brewery 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public string Website { get; set; }
        public bool Collaboration { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual ICollection<BreweryBeer> BreweryBeers { get; set;  }
        public string Image { get; set; }
        public virtual ICollection<CollaborationRequest> CollaborationRequests { get; set; }
        public virtual ICollection<Fan> Followers { get; set; }

        public virtual ICollection<Follow> Follows { get; set; }
        public string BreweryRank { get; set; }

        public virtual int LoggedInBreweryId { get; set; }



    }
}
