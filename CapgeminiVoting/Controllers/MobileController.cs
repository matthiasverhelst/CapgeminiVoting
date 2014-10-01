using CapgeminiVoting.BusinessLayer;
using CapgeminiVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapgeminiVoting.Controllers
{
    public class MobileController : Controller
    {
        public ActionResult Index()
        {
            String errMsg = TempData["ErrorMessage"] as string;
            return View("Index",null,errMsg);
        }

        public ActionResult Vote(string eventCodeString)
        {
            int eventCode;

            try {
                eventCode = Convert.ToInt32(eventCodeString);
            }
            catch {
                TempData["ErrorMessage"] = "The event ID should be numeric.";
                return RedirectToAction("Index");
            }

            IList<QuestionModel> model = CommonBusinessLayer.getQuestionsByEvent(eventCode);
            
            if (model.Count() == 0)
            {
                TempData["ErrorMessage"] = "No questions found for this event ID."; 
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult VoteSubmit(VoteModel voteResult)
        {
            //Answers valideren + door business layer laten updaten in database
            // on error: return Vote(voteResult.code);

            return VoteComplete();
        }

        public ActionResult VoteComplete()
        {
            return View();
        }
    }
}