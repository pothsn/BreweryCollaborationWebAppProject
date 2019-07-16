using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class Collaboration
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("BeerStyle")]
        public int StyleId { get; set; }
        public BeerStyle BeerStyle { get; set; }

        [ForeignKey("CollaborationRequest")]
        public int CollaborationRequestId { get; set; }
        public CollaborationRequest CollaborationRequest { get; set; }

    }
}
