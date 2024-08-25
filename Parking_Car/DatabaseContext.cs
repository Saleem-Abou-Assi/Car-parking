using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Parking_Car.Models;

namespace Parking_Car
{
    internal class DataBaseContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingSlot> ParkingSlots { get; set; }
        public DbSet<ParkingRecord> ParkingRecords { get; set; }
        public DbSet<Subscription> Subsecriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Driver>()
                .HasMany(d => d.Vehicles)
                .WithOne(v => v.Driver)
                .HasForeignKey(v => v.DriverId);

            modelBuilder.Entity<ParkingRecord>()
                .HasOne(pr => pr.Vehicle)
                .WithOne(v => v.ParkingRecord)
                .HasForeignKey<ParkingRecord>(pr => pr.VehicleId);
            
            modelBuilder.Entity<Driver>()
                .HasMany(d => d.Subscription)
                .WithOne(s => s.Driver)
                .HasForeignKey(s => s.DriverId);

            modelBuilder.Entity<ParkingRecord>()
                .HasOne(pr => pr.ParkingSlot)
                .WithOne(ps => ps.ParkingRecord)
                .HasForeignKey<ParkingRecord>(pr => pr.SlotId
                );

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Vehicle)
                .WithOne(v => v.Subscription)
                .HasForeignKey<Subscription>(s => s.VehicleId
                );


        }
  
      

    }
}
