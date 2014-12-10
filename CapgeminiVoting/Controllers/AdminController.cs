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
            IList<EventOverviewModel> model = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateEvent(EventDetailsModel @event)
        {
            bool result = false;

            if (@event != null)
                result = AdminBusinessLayer.CreateEvent(@event);

            if (result)
                return Json("Event successfully created!");
            else return Json("Event creation failed, please try again.");
        }

        public ActionResult EventDetails(int eventId)
        {
            EventDetailsModel model = AdminBusinessLayer.GetEventById(eventId);
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
        public ActionResult DeleteEvent(int eventID)
        {
            AdminBusinessLayer.DeleteEvent(eventID);
            IList<EventOverviewModel> model = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
            return View(model);
        }
    }
}