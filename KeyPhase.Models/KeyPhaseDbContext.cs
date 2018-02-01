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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasKey(b => b.ID);
        }
    }
}
