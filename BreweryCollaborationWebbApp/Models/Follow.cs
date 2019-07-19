using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Brewery")]
        public int BreweryId { get; set; }
        public Brewery brewery { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Fan")]
        public int? FanId { get; set; }
        public Fan Fan { get; set; }
        public bool IsFollowed { get; set; }
        //public bool BrokenBatBrewery { get; set; }
        //public bool MobCraftBrewery { get; set; }
        //public bool LakefrontBrewery { get; set; }

    }
}
