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
            var model = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
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
            if (AdminBusinessLayer.IsEventOwner(id, User.Identity.GetUserId()))
            {
                ViewBag.Title = Resources.Modify_event;
                var model = AdminBusinessLayer.GetEventById(id);

                if (model == null)
                {
                    return View("CreateEvent");
                }

                var sortedQuestions = model.Questions.ToList();
                sortedQuestions.Sort();
                model.Questions = sortedQuestions;

                return View("CreateEvent", model);
            }

            ViewBag.ErrorMessage = Resources.Not_your_event;
            var indexModel = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
            return View("Index", indexModel);
        }

        [HttpPost]
        public ActionResult CreateEvent(EventDetailsModel @event)
        {
            bool result = false;

            if (@event == null)
            {
                var model = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
                return View("Index", model);
            }

            var questionList = @event.Questions.ToList();
            questionList.RemoveAll(question => string.IsNullOrWhiteSpace(question.Question));
            @event.Questions = questionList;

            if (@event.Questions.Count == 0)
            {
                ModelState.Clear();
                ModelState.AddModelError(String.Empty, Resources.Please_specify_question);
                ViewBag.Title = Resources.Create_new_event;
                return View("CreateEvent", @event);
            }

            foreach (var question in @event.Questions)
            {
                var answerList = question.Answers.ToList();
                answerList.RemoveAll(answer => string.IsNullOrWhiteSpace(answer.Answer));
                question.Answers = answerList;

                if (question.Answers.Count < 2 && question.QuestionType != 2)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(String.Empty, Resources.Please_specify_answer);
                    ViewBag.Title = Resources.Create_new_event;
                    return View("CreateEvent", @event);
                }
            }

            result = AdminBusinessLayer.CreateEvent(@event);

            if (result)
            {
                ViewBag.SuccessMessage = Resources.Event_successfully_created;
                var indexModel = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
                return View("Index", indexModel);
            }
            else
            {
                return RedirectToAction("CreateEvent");
            }
        }

        [HttpPost]
        public ActionResult ModifyEvent(EventDetailsModel @event)
        {
            if (@event == null)
            {
                var model = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
                return View("Index", model);
            }

            var questionList = @event.Questions.ToList();
            questionList.RemoveAll(question => string.IsNullOrWhiteSpace(question.Question));
            @event.Questions = questionList;

            if (@event.Questions.Count == 0)
            {
                ModelState.Clear();
                ModelState.AddModelError(String.Empty, Resources.Please_specify_question);
                ViewBag.Title = Resources.Modify_event;
                return View("CreateEvent", @event);
            }

            foreach (var question in @event.Questions)
            {
                if (question.Answers != null)
                {
                    var answerList = question.Answers.ToList();
                    answerList.RemoveAll(answer => string.IsNullOrWhiteSpace(answer.Answer));
                    question.Answers = answerList;

                    if (question.Answers.Count < 2 && question.QuestionType != 2)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(String.Empty, Resources.Please_specify_answer);
                        ViewBag.Title = Resources.Modify_event;
                        return View("CreateEvent", @event);
                    }
                }
            }

            AdminBusinessLayer.ModifyEvent(@event);

            ViewBag.SuccessMessage = Resources.Event_successfully_modified;
            var indexModel = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
            return View("Index", indexModel);
        }

        public ActionResult EventDetails(int eventId)
        {
            if (AdminBusinessLayer.IsEventOwner(eventId, User.Identity.GetUserId()))
            {
                var model = AdminBusinessLayer.GetEventById(eventId);
                if (model == null)
                {
                    return Index();
                }

                return View(model);
            }

            ViewBag.ErrorMessage = Resources.Not_your_event;
            var indexModel = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
            return View("Index", indexModel);
        }

        public ActionResult EventResult(int id)
        {
            if (AdminBusinessLayer.IsEventOwner(id, User.Identity.GetUserId()))
            {
                ViewBag.EventId = id;
                return View();
            }

            ViewBag.ErrorMessage = Resources.Not_your_event;
            var indexModel = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
            return View("Index", indexModel);
        }

        public ActionResult DeleteEvent(int id)
        {
            if (AdminBusinessLayer.IsEventOwner(id, User.Identity.GetUserId()))
            {
                AdminBusinessLayer.DeleteEvent(id);
                var model = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
                ViewBag.SuccessMessage = Resources.Event_successfully_removed;
                return View("Index", model);
            }

            ViewBag.ErrorMessage = Resources.Not_your_event;
            var indexModel = AdminBusinessLayer.GetEventsByUser(User.Identity.GetUserId());
            return View("Index", indexModel);
        }

        [HttpGet]
        public JsonResult GetQuestions(int eventId)
        {
            var eventDetails = AdminBusinessLayer.GetEventById(eventId);
            return Json(eventDetails.Questions.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetQuestionResult(int questionId)
        {
            return Json(AdminBusinessLayer.GetQuestionResult(questionId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LockOrUnlockEvent(int id, bool lockEvent)
        {
            if(AdminBusinessLayer.IsEventOwner(id, User.Identity.GetUserId()))
            {
                return Json(AdminBusinessLayer.LockOrUnlockEvent(id, lockEvent));
            }

            return Json(false);
        }
    }
}