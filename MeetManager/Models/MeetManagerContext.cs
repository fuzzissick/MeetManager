using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetManager.Models
{
    public class MeetManagerContext : DbContext
    {
        public MeetManagerContext(DbContextOptions<MeetManagerContext> options)
     : base(options)
        {
            //keep this but leave it empty
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamMeet>()
                .HasKey(t => new { t.TeamId, t.MeetId});

            modelBuilder.Entity<TeamMeet>()
                .HasOne(pt => pt.Meet)
                .WithMany(p => p.TeamMeets)
                .HasForeignKey(pt => pt.MeetId);

            modelBuilder.Entity<TeamMeet>()
                .HasOne(pt => pt.Team)
                .WithMany(t => t.TeamMeets)
                .HasForeignKey(pt => pt.TeamId);
        }

    public DbSet<Meet> Meets { get; set; }
        public DbSet<Runner> Runners { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
