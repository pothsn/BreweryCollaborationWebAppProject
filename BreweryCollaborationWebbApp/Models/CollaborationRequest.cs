using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BreweryCollaborationWebbApp.Models
{
    public class CollaborationRequest
    {
        [Key]
        public int Id { get; set; }
        public string SenderName { get; set; }

        [ForeignKey("Brewery")]
        public int SenderId { get; set; }
        public Brewery Brewery { get; set; }

        public int ReceiverId { get; set; }
    }
}
