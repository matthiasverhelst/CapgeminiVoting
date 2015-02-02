using AutoMapper;
using CapgeminiVoting.DAO;
using CapgeminiVoting.DTO;
using CapgeminiVoting.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.BusinessLayer
{
    public class AdminBusinessLayer
    {
        public static EventDetailsModel GetEventById(int eventId)
        {
            EventDetailsModel model = null;
            using(DAOEvent dao = new DAOEvent())
            {
                var dtoEvent = dao.GetEventById(eventId);
                if (dtoEvent == null)
                    return null;

                model = Mapper.Map<DTOEvent, EventDetailsModel>(dtoEvent);
            }
            return model;
        }

        public static IList<EventOverviewModel> GetEventsByUser(string userName)
        {
            var result = new List<EventOverviewModel>();
            using(var dao = new DAOEvent())
            {
                var events = dao.GetEventsByUser(userName);

                foreach (var dtoEvent in events)
                {
                    var model = Mapper.Map<DTOEvent, EventOverviewModel>(dtoEvent);
                    result.Add(model);
                }
            }
            return result;
        }

        public static IList<AnswerModel> GetAnswersByQuestion(int questionId)
        {
            var result = new List<AnswerModel>();
            using (var dao = new DAOAnswer())
            {
                var answers = dao.GetAnswersByQuestion(questionId);

                foreach (var dtoAnswer in answers)
                {
                    var model = Mapper.Map<DTOAnswer, AnswerModel>(dtoAnswer);
                    result.Add(model);
                }
            }
            return result;
        }

        public static bool CreateEvent(EventDetailsModel @event)
        {
            var dtoEvent = Mapper.Map<EventDetailsModel, DTOEvent>(@event);
            dtoEvent.UserName = HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserId();
            dtoEvent.CreationDate = DateTime.Now;
            dtoEvent.Locked = true;

            var index = 1;
            foreach (var question in dtoEvent.Questions)
            {
                question.QuestionNumber = index;
                question.VoterCount = 0;
                index++;
                foreach(var answer in question.Answers)
                {
                    answer.Predefined = true;
                }
            }

            using(var daoEvent = new DAOEvent())
            {
                return daoEvent.CreateEvent(dtoEvent);
            }
        }

        public static bool DeleteEvent(int eventID)
        {
            using (var dao = new DAOEvent())
            {
                return dao.DeleteEventById(eventID);
                // DTOEvent dtoEvent = dao.DeleteEventById(eventID);
                //if (dtoEvent == null)
                  //  return null;

                //model = Mapper.Map<DTOEvent, EventDetailsModel>(dtoEvent);
            }
        }

        public static bool ModifyEvent(EventDetailsModel @event)
        {
            var dtoEvent = Mapper.Map<EventDetailsModel, DTOEvent>(@event);
            dtoEvent.UserName = HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserId();
            dtoEvent.CreationDate = DateTime.Now;

            var index = 1;
            foreach (var question in dtoEvent.Questions)
            {
                question.EventId = @event.Id;
                question.QuestionNumber = index;
                index++;
                foreach (DTOAnswer answer in question.Answers)
                {
                    answer.QuestionId = question.Id;
                    answer.Predefined = true;
                }
            }

            using(var daoEvent = new DAOEvent())
            {
                return daoEvent.ModifyEvent(dtoEvent);
            }
        }

        public static bool IsEventOwner(int eventId, string userName)
        {
            using (var daoEvent = new DAOEvent())
            {
                var dtoEvent = daoEvent.GetEventById(eventId);
                if (dtoEvent == null)
                    return false;
                return userName.Equals(dtoEvent.UserName);
            }
        }

        public static QuestionResultModel GetQuestionResult(int questionId)
        {
            DTOQuestion dtoQuestion = null;
            using (var daoQuestion = new DAOQuestion())
            {
                dtoQuestion = daoQuestion.GetQuestionById(questionId);

                var questionResult = new QuestionResultModel();
                questionResult.Question = dtoQuestion.Question;
                questionResult.VoterCount = dtoQuestion.VoterCount;
                questionResult.AnswerResult = new List<AnswerResultModel>();

                foreach (var answer in dtoQuestion.Answers)
                {
                    questionResult.AnswerResult.Add(Mapper.Map<DTOAnswer, AnswerResultModel>(answer));
                }

                return questionResult;
            }
        }

        public static bool LockOrUnlockEvent(int id, bool lockOrUnlock)
        {
            using(var daoEvent = new DAOEvent())
            {
                return daoEvent.LockOrUnlockEvent(id, lockOrUnlock);
            }
        }
    }
}