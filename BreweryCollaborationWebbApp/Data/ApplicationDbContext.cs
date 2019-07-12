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


        public DbSet<BeerFanTaste> BeerFanTaste { get; set; }
        public DbSet<BeerStyle> BeerStyle { get; set; }    
        public DbSet<BreweryBeer> BreweryBeer { get; set; }
        public DbSet<BreweryFollow> BreweryFollow { get; set; }
        public DbSet<Collaboration> Collaboration { get; set; }
        public DbSet<CollaborationJunction> CollaborationJunction { get; set; }
        public DbSet<Fan> Fan { get; set; }
        public DbSet<FanFollow> FanFollow { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Brewery> Brewery { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
