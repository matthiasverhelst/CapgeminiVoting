using AutoMapper;
using CapgeminiVoting.DAO;
using CapgeminiVoting.DTO;
using CapgeminiVoting.Models;
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
                IList<DTOAnswer> answers = dao.getAnswersByQuestion(questionId);
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
            using(DAOEvent daoEvent = new DAOEvent())
            {
                return daoEvent.CreateEvent(dtoEvent);
            }
        }
    }
}