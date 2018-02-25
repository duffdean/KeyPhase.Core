using KeyPhase.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPhase.Models
{
    public class KeyPhaseDbContext : DbContext
    {
        public KeyPhaseDbContext(DbContextOptions<KeyPhaseDbContext> options) : base(options) { }

        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<UserProject> UserProject { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<ProjectTask> ProjectTask{ get; set; }
        public virtual DbSet<Phase> Phase { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<UserProject>()
                .HasKey(x => new { x.UserID, x.ProjectID });

            modelBuilder.Entity<Task>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<ProjectTask>()
                .HasKey(x => new { x.ProjectID, x.TaskID});

            modelBuilder.Entity<Phase>()
                .HasKey(b => b.ID);
        }
    }
}
