using CapgeminiVoting.BusinessLayer;
using CapgeminiVoting.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapgeminiVoting.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            IList<EventOverviewModel> model = AdminBusinessLayer.getEventsByUser(User.Identity.GetUserId());
            return View(model);
        }

        public ActionResult CreateEvent()
        {
            return View();
        }

        public ActionResult EventDetails(int eventId)
        {
            EventDetailsModel model = AdminBusinessLayer.getEventById(eventId);
            if (model == null)
            {
                return Index();
            }

            return View(model);
        }

        public ActionResult ResultDetails(int eventId)
        {
            //TODO: Return results of this specific event using ResultModel.
            return View();
        }
    }
}