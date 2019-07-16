using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class CollaborationJunction
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Collaborations")]
        public int CollaborationId { get; set; }
        public Collaboration Collaboration { get; set; }

        [ForeignKey("Brewery")]
        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }

    }
}
