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
        public virtual DbSet<ProjectTaskPhase> ProjectTaskPhase { get; set; }
        public virtual DbSet<ProjectHistory> ProjectHistory { get; set; }
        public virtual DbSet<TaskHistory> TaskHistory { get; set; }
        public virtual DbSet<PhaseUser> PhaseUser { get; set; }
        public virtual DbSet<PhaseCore> PhaseCore { get; set; }
        public virtual DbSet<UserTask> UserTask{ get; set; }
        public virtual DbSet<Team> Team{ get; set; }
        public virtual DbSet<TeamProject> TeamProject{ get; set; }
        public virtual DbSet<TeamUser> TeamUser{ get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<UserReport> UserReport{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<UserProject>()
                .HasKey(x => new { x.UserID, x.ProjectID });

            modelBuilder.Entity<ProjectTaskPhase>()
               .HasKey(x => new { x.PhaseID, x.ProjectID});

            modelBuilder.Entity<Task>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<ProjectTask>()
                .HasKey(x => new { x.ProjectID, x.TaskID});

            modelBuilder.Entity<Phase>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<ProjectHistory>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<TaskHistory>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<PhaseUser>()
                .HasKey(x => new { x.PhaseID, x.UserID});

            modelBuilder.Entity<PhaseCore>()
                .HasKey(x => new { x.PhaseID, x.UserID });

            modelBuilder.Entity<UserTask>()
                .HasKey(x => new { x.UserID, x.TaskID});

            modelBuilder.Entity<Team>()
                 .HasKey(b => b.ID);

            modelBuilder.Entity<TeamProject>()
                .HasKey(x => new { x.ProjectID, x.TeamID });

            modelBuilder.Entity<TeamUser>()
                .HasKey(x => new { x.TeamID, x.UserID });

            modelBuilder.Entity<Report>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<UserReport>()
                .HasKey(x => new { x.UserID, x.ReportID});
        }
    }
}
