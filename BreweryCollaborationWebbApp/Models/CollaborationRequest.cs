﻿using System;
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

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Brewery")]
        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }
    }
}
