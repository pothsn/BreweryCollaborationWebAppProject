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
        [NotMapped]
        public List<Collaboration> Collaborations { get; set; }
        [NotMapped]
        public List<Fan> Fans { get; set; }


    }
}
