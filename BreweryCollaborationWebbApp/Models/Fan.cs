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
        //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<BeerFanTaste> BeerFanTastes { get; set; }

    }
}
