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
        [HttpGet]
        public ActionResult Index()
        {
            return Index(string.Empty);
        }

        public ActionResult Index(string message)
        {
            ViewBag.message = message;
            IList<EventOverviewModel> model = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateEvent()
        {
            ViewBag.Title = Resources.Create_new_event;
            return View();
        }

        [HttpGet]
        public ActionResult ModifyEvent(int id)
        {
            ViewBag.Title = Resources.Modify_event;
            EventDetailsModel model = AdminBusinessLayer.GetEventById(id);

            if (model == null)
            {
                return View("CreateEvent");
            }

            var sortedQuestions = model.Questions.ToList();
            sortedQuestions.Sort();
            model.Questions = sortedQuestions;
            
            return View("CreateEvent", model);
        }

        [HttpPost]
        public ActionResult CreateEvent(EventDetailsModel @event)
        {
            bool result = false;

            if (@event != null)
            {
                var questionList = @event.Questions.ToList();
                questionList.RemoveAll(question => string.IsNullOrWhiteSpace(question.Question));
                foreach (var question in @event.Questions)
                {
                    var answerList = question.Answers.ToList();
                    answerList.RemoveAll(answer => string.IsNullOrWhiteSpace(answer.Answer));
                    question.Answers = answerList;
                }
            }

            result = AdminBusinessLayer.CreateEvent(@event);

            if (result)
                return RedirectToAction("Index");
            else return RedirectToAction("CreateEvent");
        }

        [HttpPost]
        public ActionResult ModifyEvent(EventDetailsModel @event)
        {
            if (@event == null)
            {
                RedirectToAction("Index");
            }

            var questionList = @event.Questions.ToList();
            questionList.RemoveAll(question => string.IsNullOrWhiteSpace(question.Question));
            foreach (var question in @event.Questions)
            {
                if (question.Answers != null)
                {
                    var answerList = question.Answers.ToList();
                    answerList.RemoveAll(answer => string.IsNullOrWhiteSpace(answer.Answer));
                    question.Answers = answerList;
                }
            }

            AdminBusinessLayer.ModifyEvent(@event);
            return RedirectToAction("Index");
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
        public ActionResult DeleteEvent(int id)
        {
            AdminBusinessLayer.DeleteEvent(id);
            IList<EventOverviewModel> model = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
            return View("Index", model);
        }
    }
}