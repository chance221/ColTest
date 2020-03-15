using System;
using System.Collections.Generic;
using System.Text;
using Collegiate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Collegiate.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<DriverOffer> DriverOffers { get; set; }

        public DbSet<Preference> Preferences { get; set; }

        public DbSet<RiderRequest> RiderRequests { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TripUsers>()
                .HasKey(tu => new { tu.UserId, tu.TripId });
            modelBuilder.Entity<TripUsers>()
                .HasOne(tu => tu.User)
                .WithMany(u => u.TripUsers)
                .HasForeignKey(tu => tu.UserId);
            modelBuilder.Entity<TripUsers>()
                .HasOne(tu => tu.Trip)
                .WithMany(t => t.TripUsers)
                .HasForeignKey(tu => tu.TripId);
        }
    }
}
