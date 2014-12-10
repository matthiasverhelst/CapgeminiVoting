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
            ViewBag.Message = TempData["Message"] as string;
            return View();
        }

        public ActionResult Vote(QuestionRequestModel questionRequest)
        {
            int eventCode;
            QuestionInfoModel model = new QuestionInfoModel();

            if (String.IsNullOrEmpty(questionRequest.EventCode))
            {
                TempData["Message"] = "No event code filled in.";
                return RedirectToAction("Index");
            } 
            
            try 
            {
                eventCode = Convert.ToInt32(questionRequest.EventCode);
            }
            catch
            {
                TempData["Message"] = Resources.Err_event_not_numeric;
                return RedirectToAction("Index");
            }

            IList<QuestionModel> questionList = CommonBusinessLayer.getQuestionsByEvent(eventCode);

            if (questionList.Count() >= questionRequest.QuestionNumber && questionRequest.QuestionNumber > 0)
            {
                model.EventCode = questionRequest.EventCode;
                model.QuestionData = questionList.ElementAt(questionRequest.QuestionNumber - 1);
            }
            else if (questionList.Count() < questionRequest.QuestionNumber)
            {
                TempData["Message"] = Resources.Msg_all_questions_processed;
                return RedirectToAction("VoteComplete");
            }
            else if (questionList.Count() == 0)
            {
                TempData["Message"] = Resources.Err_no_questions_found_for_event; 
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = Resources.Err_no_questions_found_for_event;
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult VoteSubmit(VoteResultModel voteResult)
        {
            int eventCode;
            QuestionRequestModel model = new QuestionRequestModel();

            try
            {
                eventCode = Convert.ToInt32(voteResult.EventCode);
            }
            catch
            {
                TempData["Message"] = Resources.Err_event_not_numeric;
                return RedirectToAction("Index");
            }

            if (MobileBusinessLayer.SetAnswerCount(eventCode, voteResult.QuestionNumber, voteResult.Answer) != true)
            {
                TempData["Message"] = Resources.Err_unable_to_update_answer_on_db;
                return RedirectToAction("Index");
            }

            model.EventCode = voteResult.EventCode;
            model.QuestionNumber = voteResult.QuestionNumber + 1;

            return RedirectToAction("Vote", model);
        }

        public ActionResult VoteComplete()
        {
            ViewBag.Message = TempData["Message"] as string;
            return View();
        }
    }
}