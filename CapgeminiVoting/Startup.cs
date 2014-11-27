﻿using Microsoft.Owin;
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
using System.Data.Entity.Validation;

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
            using (VotingContext db = new VotingContext())
            {
                db.Database.Initialize(false);
            }
        }

        private class DbInitializer : DropCreateDatabaseAlways<VotingContext>
        {
            protected override void Seed(VotingContext context)
            {
                var userName = string.Empty;
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Database.Initialize(false);
                    userName = db.Users.FirstAsync().Result.Id;
                }

                var events = new List<DTOEvent>();
                var dtoEvent = new DTOEvent()
                               {
                                   name = "Sprint 1 Questionaire",
                                   startDate = DateTime.Now,
                                   endDate = DateTime.Now.AddDays(10),
                                   description = "Bla.",
                                   userName = userName,
                                   questions = new List<DTOQuestion>()
                                                       {
                                                           new DTOQuestion
                                                           {
                                                               question = "Was de pizza te pikant?",
                                                               questionType = 0,
                                                               questionNumber = 1,
                                                               answers = new List<DTOAnswer>()
                                                               {
                                                                   new DTOAnswer
                                                                   {
                                                                       answer = "Yes.",
                                                                       predefined = true
                                                                   },
                                                                   new DTOAnswer
                                                                   {
                                                                       answer = "FUCK YES.",
                                                                       predefined = true
                                                                   }
                                                               }
                                                           },
                                                           new DTOQuestion
                                                           {
                                                               question = "Is de Matti weird?",
                                                               questionType = 1,
                                                               questionNumber = 2,
                                                               answers = new List<DTOAnswer>()
                                                               {
                                                                   new DTOAnswer
                                                                   {
                                                                       answer = "Yes.",
                                                                       predefined = true
                                                                   },
                                                                   new DTOAnswer
                                                                   {
                                                                       answer = "FUCK YES.",
                                                                       predefined = true
                                                                   }
                                                               }
                                                           },
                                                           new DTOQuestion
                                                           {
                                                               question = "Welk gevoel heb je bij het aankomen van de pizzaman?",
                                                               questionType = 2,
                                                               questionNumber = 3,
                                                               answers = new List<DTOAnswer>()
                                                           }
                                                       }
                                };
                events.Add(dtoEvent);

                events.ForEach(p => context.Events.Add(p));
                context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Event', RESEED, 100000)");
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
        }

        private class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new ApplicationUserManager(userStore);
                ApplicationUser user = new ApplicationUser();
                user.Email = "dummy@gmail.com";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.LockoutEndDateUtc = null;
                user.PhoneNumber = "027081777";
                user.PhoneNumberConfirmed = true;
                user.SecurityStamp = "kdafskfdjjkf";
                user.UserName = "dummy@gmail.com";

                userManager.Create(user, "Test123");
            }
        }
    }
}
