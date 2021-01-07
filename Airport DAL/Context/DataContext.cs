using Airport_Common.Models;
using Airport_DAL.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./Database.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbRoute>().Property(p => p.RouteArray)
            .HasConversion(
                routeIds => JsonConvert.SerializeObject(routeIds),
                routeIds => JsonConvert.DeserializeObject<List<List<int>>>(routeIds));
        }


        public DbSet<DbAirport> Airports { get; set; }
        public DbSet<DbStation> Stations { get; set; }
        public DbSet<DbPlane> Planes { get; set; }
        public DbSet<PlaneLog> PlanesLog { get; set; }

    }
}
