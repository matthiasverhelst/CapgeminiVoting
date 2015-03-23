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
            return View();
        }

        public ActionResult Vote(QuestionRequestModel questionRequest)
        {
            int eventCode;
            QuestionInfoModel model = new QuestionInfoModel();

            if (string.IsNullOrEmpty(questionRequest.EventCode))
            {
                ViewBag.Message = Resources.Err_no_event_code;
                return View("Index");
            } 
            
            try 
            {
                eventCode = Convert.ToInt32(questionRequest.EventCode);
            }
            catch
            {
                ViewBag.Message = Resources.Err_event_not_numeric;
                return View("Index");
            }

            if (MobileBusinessLayer.IsEventLocked(eventCode))
            {
                ViewBag.Message = Resources.Event_locked;
                return View("Index");
            }

            IList<QuestionModel> questionList = CommonBusinessLayer.GetQuestionsByEvent(eventCode);

            if (questionList.Count() >= questionRequest.QuestionNumber && questionRequest.QuestionNumber > 0)
            {
                model.EventCode = questionRequest.EventCode;
                model.QuestionData = questionList.ElementAt(questionRequest.QuestionNumber - 1);
            }
            else if (questionList.Count() < questionRequest.QuestionNumber)
            {
                ViewBag.Message = Resources.Msg_all_questions_processed;
                return View("VoteComplete");
            }
            else if (questionList.Count() == 0)
            {
                ViewBag.Message = Resources.Err_no_questions_found_for_event; 
                return View("Index");
            }
            else
            {
                ViewBag.Message = Resources.Err_no_questions_found_for_event;
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
                ViewBag.Message = Resources.Err_event_not_numeric;
                return View("Index");
            }

            if (voteResult.AnswerId == null && voteResult.FreeTextAnswer == null)
            {
                model.EventCode = voteResult.EventCode;
                model.QuestionNumber = voteResult.QuestionNumber;
                return RedirectToAction("Vote", model);
            }

            if (MobileBusinessLayer.SetAnswerCount(eventCode, voteResult.QuestionNumber, voteResult.AnswerId, voteResult.FreeTextAnswer) != true)
            {
                ViewBag.Message = Resources.Err_unable_to_update_answer_on_db;
                return View("Index");
            }

            model.EventCode = voteResult.EventCode;
            model.QuestionNumber = voteResult.QuestionNumber + 1;

            return RedirectToAction("Vote", model);
        }

        public ActionResult VoteComplete()
        {
            return View();
        }
    }
}