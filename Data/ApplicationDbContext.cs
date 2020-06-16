using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tweeter.Domain;

namespace Tweeter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TweetLike> TweetLikes { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tweet>().HasOne(b => b.User).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>().HasOne(a => a.User).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TweetLike>().HasOne(b => b.User).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CommentLike>().HasOne(b => b.User).WithOne().OnDelete(DeleteBehavior.NoAction);

        }
    }
}
