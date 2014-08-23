using CapgeminiVoting.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Contexts
{
    public class VotingContext : DbContext
    {
        public DbSet<DTOEvent> events { get; set; }
        public DbSet<DTOQuestion> questions { get; set; }
        public DbSet<DTOAnswer> answers { get; set; }
        public DbSet<DTOUser> users { get; set; }

        public VotingContext()
            : base("VotingContext")
        {
            
        }
    }
}