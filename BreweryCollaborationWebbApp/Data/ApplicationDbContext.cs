using System;
using System.Collections.Generic;
using System.Text;
using BreweryCollaborationWebbApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BreweryCollaborationWebbApp.Models;

namespace BreweryCollaborationWebbApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

        }


        public DbSet<BeerFanTaste> BeerFanTaste { get; set; }
        public DbSet<BeerStyle> BeerStyle { get; set; }    
        public DbSet<BreweryBeer> BreweryBeer { get; set; }
        public DbSet<Collaboration> Collaboration { get; set; }
        public DbSet<CollaborationRequest> CollaborationRequest { get; set; }
        public DbSet<Fan> Fan { get; set; }      
        public DbSet<Review> Review { get; set; }
        public DbSet<Brewery> Brewery { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<BreweryCollaborationWebbApp.Models.Follow> Follow { get; set; }
    }
}
