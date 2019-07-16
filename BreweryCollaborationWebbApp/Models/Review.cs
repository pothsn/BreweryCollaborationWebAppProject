using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class Review
    {

        [Key]
        public int Id { get; set; }

        public string ReviewText { get; set; }

        public int Rating { get; set; }

        [ForeignKey("Collaborations")]
        public int CollaborationId { get; set; }
        public Collaboration Collaboration { get; set; }

        [ForeignKey("Fans")]
        public int FanId { get; set; }
        public Fan Fan { get; set; }


    }
}
