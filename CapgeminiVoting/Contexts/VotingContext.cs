using CapgeminiVoting.DTO;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Contexts
{
    public class VotingContext : DbContext
    {
        public DbSet<DTOEvent> Events { get; set; }
        public DbSet<DTOQuestion> Questions { get; set; }
        public DbSet<DTOAnswer> Answers { get; set; }

        public VotingContext() : base("VotingContext")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(u => u.UserId);
            modelBuilder.Entity<IdentityUserRole>().HasKey<string>(u => u.UserId);
        }
    }
}