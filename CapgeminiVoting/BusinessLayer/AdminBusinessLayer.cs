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
                DTOEvent dtoEvent = dao.GetEventById(eventId);
                if (dtoEvent == null)
                    return null;

                model = Mapper.Map<DTOEvent, EventDetailsModel>(dtoEvent);
            }
            return model;
        }

        public static IList<EventOverviewModel> GetEventsByUser(string userName)
        {
            List<EventOverviewModel> result = new List<EventOverviewModel>();
            using(DAOEvent dao = new DAOEvent())
            {
                IList<DTOEvent> events = dao.GetEventsByUser(userName);
                if (events.Count == 0)
                    return result;

                foreach (DTOEvent dtoEvent in events)
                {
                    EventOverviewModel model = Mapper.Map<DTOEvent, EventOverviewModel>(dtoEvent);
                    result.Add(model);
                }
            }
            return result;
        }

        public static IList<AnswerModel> GetAnswersByQuestion(int questionId)
        {
            List<AnswerModel> result = new List<AnswerModel>();
            using (DAOAnswer dao = new DAOAnswer())
            {
                IList<DTOAnswer> answers = dao.GetAnswersByQuestion(questionId);
                if (answers.Count == 0)
                    return result;

                foreach (DTOAnswer dtoAnswer in answers)
                {
                    AnswerModel model = Mapper.Map<DTOAnswer, AnswerModel>(dtoAnswer);
                    result.Add(model);
                }
            }
            return result;
        }

        public static bool CreateEvent(EventDetailsModel @event)
        {
            DTOEvent dtoEvent = Mapper.Map<EventDetailsModel, DTOEvent>(@event);
            dtoEvent.UserName = HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserId();
            dtoEvent.CreationDate = DateTime.Now;

            var index = 1;
            foreach (DTOQuestion question in dtoEvent.Questions)
            {
                question.QuestionNumber = index;
                index++;
                foreach(DTOAnswer answer in question.Answers)
                {
                    answer.Predefined = true;
                }
            }

            using(DAOEvent daoEvent = new DAOEvent())
            {
                return daoEvent.CreateEvent(dtoEvent);
            }
        }

        public static bool DeleteEvent(int eventID)
        {
            using (DAOEvent dao = new DAOEvent())
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
            DTOEvent dtoEvent = Mapper.Map<EventDetailsModel, DTOEvent>(@event);
            dtoEvent.UserName = HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserId();
            dtoEvent.CreationDate = DateTime.Now;

            var index = 1;
            foreach (DTOQuestion question in dtoEvent.Questions)
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

            using(DAOEvent daoEvent = new DAOEvent())
            {
                return daoEvent.ModifyEvent(dtoEvent);
            }
        }
    }
}