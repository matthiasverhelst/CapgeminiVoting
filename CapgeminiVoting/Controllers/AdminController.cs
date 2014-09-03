using CapgeminiVoting.BusinessLayer;
using CapgeminiVoting.Models;
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
            //TODO: Show list of all Events this user has created
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