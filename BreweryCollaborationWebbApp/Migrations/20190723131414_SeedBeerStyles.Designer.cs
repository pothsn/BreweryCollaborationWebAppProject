﻿// <auto-generated />
using System;
using BreweryCollaborationWebbApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BreweryCollaborationWebbApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190723131414_SeedBeerStyles")]
    partial class SeedBeerStyles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.BeerFanTaste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ale");

                    b.Property<int>("BeerStyleId");

                    b.Property<int>("FanId");

                    b.Property<bool>("IndiaPaleAle");

                    b.Property<bool>("Lager");

                    b.Property<bool>("PaleAle");

                    b.Property<bool>("Pilsner");

                    b.Property<bool>("Porter");

                    b.Property<bool>("Saison");

                    b.Property<bool>("Sour");

                    b.Property<bool>("Stout");

                    b.Property<bool>("WheatBeer");

                    b.HasKey("Id");

                    b.HasIndex("BeerStyleId");

                    b.HasIndex("FanId");

                    b.ToTable("BeerFanTaste");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.BeerStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("BeerStyle");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Brewery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("ApplicationId");

                    b.Property<string>("BreweryRank");

                    b.Property<string>("City");

                    b.Property<bool>("Collaboration");

                    b.Property<string>("Image");

                    b.Property<double>("Latitude");

                    b.Property<int>("LoggedInBreweryId");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.Property<string>("Website");

                    b.Property<int>("Zipcode");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Brewery");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.BreweryBeer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BreweryId");

                    b.Property<string>("Name");

                    b.Property<int>("StyleId");

                    b.HasKey("Id");

                    b.HasIndex("BreweryId");

                    b.HasIndex("StyleId");

                    b.ToTable("BreweryBeer");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Collaboration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrewSite");

                    b.Property<int?>("CollaborationId");

                    b.Property<int>("CollaborationRequestId");

                    b.Property<string>("Name");

                    b.Property<int>("StyleId");

                    b.Property<DateTime>("Updated");

                    b.Property<DateTime>("WhenCreated");

                    b.HasKey("Id");

                    b.HasIndex("CollaborationId");

                    b.HasIndex("CollaborationRequestId");

                    b.HasIndex("StyleId");

                    b.ToTable("Collaboration");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.CollaborationRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ReceiverId");

                    b.Property<string>("ReceiverName");

                    b.Property<int>("SenderId");

                    b.Property<string>("SenderName");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("CollaborationRequest");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Fan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("ApplicationId");

                    b.Property<int?>("BreweryId");

                    b.Property<string>("City");

                    b.Property<double>("Latitude");

                    b.Property<bool>("LikesAle");

                    b.Property<bool>("LikesIPA");

                    b.Property<bool>("LikesLager");

                    b.Property<bool>("LikesPaleAle");

                    b.Property<bool>("LikesPilsner");

                    b.Property<bool>("LikesPorter");

                    b.Property<bool>("LikesSaison");

                    b.Property<bool>("LikesSour");

                    b.Property<bool>("LikesStout");

                    b.Property<bool>("LikesWheatBeer");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.Property<int>("Zipcode");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("BreweryId");

                    b.ToTable("Fan");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Follow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationId");

                    b.Property<int?>("BreweryFollowerId");

                    b.Property<int>("BreweryId");

                    b.Property<int?>("FanFollowerId");

                    b.Property<bool>("IsFollowed");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("BreweryId");

                    b.HasIndex("FanFollowerId");

                    b.ToTable("Follow");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CollaborationId");

                    b.Property<int>("FanId");

                    b.Property<int>("Rating");

                    b.Property<string>("ReviewText");

                    b.HasKey("Id");

                    b.HasIndex("CollaborationId");

                    b.HasIndex("FanId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Name");

                    b.ToTable("ApplicationUser");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.BeerFanTaste", b =>
                {
                    b.HasOne("BreweryCollaborationWebbApp.Models.BeerStyle", "BeerStyle")
                        .WithMany("BeerFanTastes")
                        .HasForeignKey("BeerStyleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BreweryCollaborationWebbApp.Models.Fan", "Fan")
                        .WithMany("BeerFanTastes")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Brewery", b =>
                {
                    b.HasOne("BreweryCollaborationWebbApp.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.BreweryBeer", b =>
                {
                    b.HasOne("BreweryCollaborationWebbApp.Models.Brewery", "Brewery")
                        .WithMany("BreweryBeers")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BreweryCollaborationWebbApp.Models.BeerStyle", "BeerStyle")
                        .WithMany("BreweryBeers")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Collaboration", b =>
                {
                    b.HasOne("BreweryCollaborationWebbApp.Models.Collaboration")
                        .WithMany("Collaborations")
                        .HasForeignKey("CollaborationId");

                    b.HasOne("BreweryCollaborationWebbApp.Models.CollaborationRequest", "CollaborationRequest")
                        .WithMany()
                        .HasForeignKey("CollaborationRequestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BreweryCollaborationWebbApp.Models.BeerStyle", "BeerStyle")
                        .WithMany()
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.CollaborationRequest", b =>
                {
                    b.HasOne("BreweryCollaborationWebbApp.Models.Brewery", "Brewery")
                        .WithMany("CollaborationRequests")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Fan", b =>
                {
                    b.HasOne("BreweryCollaborationWebbApp.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationId");

                    b.HasOne("BreweryCollaborationWebbApp.Models.Brewery")
                        .WithMany("Followers")
                        .HasForeignKey("BreweryId");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Follow", b =>
                {
                    b.HasOne("BreweryCollaborationWebbApp.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationId");

                    b.HasOne("BreweryCollaborationWebbApp.Models.Brewery", "brewery")
                        .WithMany("Follows")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BreweryCollaborationWebbApp.Models.Fan", "Fan")
                        .WithMany("Follows")
                        .HasForeignKey("FanFollowerId");
                });

            modelBuilder.Entity("BreweryCollaborationWebbApp.Models.Review", b =>
                {
                    b.HasOne("BreweryCollaborationWebbApp.Models.Collaboration", "Collaboration")
                        .WithMany()
                        .HasForeignKey("CollaborationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BreweryCollaborationWebbApp.Models.Fan", "Fan")
                        .WithMany()
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
