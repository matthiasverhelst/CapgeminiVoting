using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapgeminiVoting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //TODO: Page should not trigger login. Should ask for event ID (this can be directly added to redirect link in view -> Vote/"ID"
            return View();
        }

        [HttpGet]
        [ActionName("Vote")]
        public ActionResult VoteGet(int eventId)
        {
            //TODO: Page should provide vote form for this specific eventId, using VoteModel
            return View();
        }

        [HttpPost]
        [ActionName("Vote")]
        public ActionResult VotePost(int eventId)
        {
            //TODO: Page should capture input of votes, using VoteModel
            return View();
        }
    }
}