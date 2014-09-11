using CapgeminiVoting.Contexts;
using CapgeminiVoting.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CapgeminiVoting
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<VotingContext>(new DbInitializer());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private class DbInitializer : DropCreateDatabaseAlways<VotingContext>
        {
            protected override void Seed(VotingContext context)
            {
                var events = new List<DTOEvent>
                {
                    new DTOEvent       {id = 1,
                                        code = 69,
                                        name = "Sprint 1 Questionaire",
                                        startDate = DateTime.Now,
                                        endDate = DateTime.Now.AddDays(10),
                                        description = "Bla.",
                                        userName = "Tom"
                    }
                };

                events.ForEach(p => context.events.Add(p));

                var questions = new List<DTOQuestion>
                {
                    new DTOQuestion {   id = 1,
                                        question = "Was de pizza te pikant?",
                                        questionType = 0,
                                        eventId = 1
                    }
                };

                questions.ForEach(p => context.questions.Add(p));


                context.SaveChanges();
            }
        }

    }
}
