using Microsoft.Owin;
using Owin;
using CapgeminiVoting.Contexts;
using CapgeminiVoting.DTO;
using CapgeminiVoting.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Collections.Generic;
using System;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartupAttribute(typeof(CapgeminiVoting.Startup))]
namespace CapgeminiVoting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
            Database.SetInitializer<VotingContext>(new DbInitializer());
        }

        private class DbInitializer : DropCreateDatabaseAlways<VotingContext>
        {
            protected override void Seed(VotingContext context)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Database.Initialize(false);
                }

                var dtoQuestions = new List<DTOQuestion>
                {
                    new DTOQuestion
                    {
                        id = 1,
                        question = "Was de pizza te pikant?",
                        questionType = 0,
                        eventId = 100000
                    }
                };

                var events = new List<DTOEvent>
                {
                    new DTOEvent
                    {
                        id = 100000,
                        name = "Sprint 1 Questionaire",
                        startDate = DateTime.Now,
                        endDate = DateTime.Now.AddDays(10),
                        description = "Bla.",
                        userName = "Tom",
                        questions = dtoQuestions
                    }
                };
                events.ForEach(p => context.Events.Add(p));
                context.SaveChanges();
            }
        }

        private class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ApplicationUser user = new ApplicationUser();
                user.Id = "Tom";
                user.Email = "dummy@gmail.com";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.LockoutEndDateUtc = null;
                user.PhoneNumber = "027081777";
                user.PhoneNumberConfirmed = true;
                user.SecurityStamp = "kdafskfdjjkf";
                user.UserName = "Tom";

                userManager.Create(user, "Test123");
            }
        }
    }
}
