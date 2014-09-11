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
            //hello
            return View();
        }

        public ActionResult Vote(int eventCode)
        {
            IList<QuestionModel> model = CommonBusinessLayer.getQuestionsByEvent(eventCode);
            
            if (model == null)
            {
                return Index();
            }

            return View(model);
        }

        public ActionResult VoteSubmit(VoteModel voteResult)
        {
            //answers valideren + door business layer laten updaten in database

            // on error: return Vote(voteResult.code);

            return VoteComplete();
        }

        public ActionResult VoteComplete()
        {
            return View();
        }
    }
}