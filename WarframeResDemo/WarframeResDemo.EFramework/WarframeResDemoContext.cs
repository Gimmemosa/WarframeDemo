using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeResDemo.Data.Entities;

namespace WarframeResDemo.EFr
{
    public class WarframeResDemoContext : DbContext
    {
        public WarframeResDemoContext() : base("WarframeResDemo.Ef.WarframeResDemoContext")
        { }

        public DbSet<Planet> Planets { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Fraction> Fractions { get; set; }
        public DbSet<MissionType> MissionTypes { get; set; }
        public DbSet<ExcavationType> ExcavationTypes { get; set; }
        public DbSet<SurvivalType> SurvivalTypes { get; set; }
        public DbSet<PlanetResource> PlanetResource { get; set; }
        public DbSet<EndedMissions> EndedMissions { get; set; }
        public DbSet<PausedMission> PausedMissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<PlanetResource>()
             .HasKey(t => new { t.PlanetId, t.ResourceId });

            modelBuilder.Entity<PlanetResource>()
                .HasRequired(pt => pt.Planet)
                .WithMany(p => p.PlanetResource)
                .HasForeignKey(pt => pt.PlanetId);

            //modelBuilder.Entity<PlanetResource>()
            //    .HasRequired(pt => pt.Resource)
            //    .WithMany(t => t.PlanetResource)
            //    .HasForeignKey(pt => pt.ResourceId);
        }
    }
}
