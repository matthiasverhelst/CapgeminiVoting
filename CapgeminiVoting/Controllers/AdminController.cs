using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapgeminiVoting.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            //TODO: Page should trigger login. After that, show list of all Events this user has created
            return View();
        }

        public ActionResult EventDetails(int eventId)
        {
            //TODO: Page should provide details of one selected event (using EventModel).
            return View();
        }

        public ActionResult ResultDetails(int eventId)
        {
            //TODO: Page should return results of this specific event using ResultModel.
            return View();
        }
    }
}