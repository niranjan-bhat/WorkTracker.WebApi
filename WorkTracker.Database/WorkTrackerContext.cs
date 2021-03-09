using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Database.Models;

namespace WorkTracker.Database
{
    public class WorkTrackerContext : DbContext
    {
        public WorkTrackerContext(DbContextOptions<WorkTrackerContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().HasKey(x => x.Id);

            modelBuilder.Entity<Owner>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Worker>().HasKey(x => x.Id);
            modelBuilder.Entity<Worker>().HasAlternateKey(c => new { c.Name, c.OwnerId });
            modelBuilder.Entity<Worker>()
                .HasIndex(u => u.Mobile)
                .IsUnique();

            modelBuilder.Entity<Worker>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Worker>().Property(x => x.Mobile).IsRequired();

            modelBuilder.Entity<Job>().HasKey(x => x.Id);
            modelBuilder.Entity<Job>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Job>().HasAlternateKey(c => new { c.Name, c.OwnerId });


            modelBuilder.Entity<Job>().HasMany(x => x.Assignments).WithMany(x => x.Jobs)
                .UsingEntity<Dictionary<string, object>>("AssignmentJob",
                    j => j.HasOne<Assignment>().WithMany().OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<Job>().WithMany().OnDelete(DeleteBehavior.Restrict));

            modelBuilder.Entity<Comment>().HasKey(x => x.Id);
            modelBuilder.Entity<Comment>().Property(x => x.OwnerComment).IsRequired();

            modelBuilder.Entity<Assignment>().HasKey(x => x.Id);
            modelBuilder.Entity<Assignment>().Property(x => x.WorkerId).IsRequired();
            modelBuilder.Entity<Assignment>().Property(x => x.AssignedDate).IsRequired();
            modelBuilder.Entity<Assignment>().HasAlternateKey(c => new { c.AssignedDate, c.WorkerId });
            
        }
    }
}
