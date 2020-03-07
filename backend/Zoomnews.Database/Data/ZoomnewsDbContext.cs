using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Zoomnews.Database.Models;

namespace Zoomnews.Database.Data
{
    public class ZoomnewsDbContext: DbContext
    {
        public ZoomnewsDbContext(DbContextOptions<ZoomnewsDbContext> options) : base(options)
        {
        }

        public DbSet<Audit> Audit { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Media> Medias { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>().ToTable("Audit");
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Media>().ToTable("Media");
           
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Children)
                .WithOne(c => c.ParentCategory)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Children)
                .WithOne(c => c.ParentComment)
                .IsRequired().OnDelete(DeleteBehavior.Restrict);

            // create indexes for tables
            modelBuilder.Entity<Article>()
                .HasIndex(a => new { a.Title, a.SEOName })
                .IsUnique(true);
            modelBuilder.Entity<Comment>()
                .HasIndex(c => new { c.Content})
                .IsUnique(true);
            modelBuilder.Entity<Media>()
                .HasIndex(m => new { m.Name, m.Id })
                .IsUnique(true);
        }
    }
}
