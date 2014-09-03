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
        public static EventDetailsModel getEventById(int eventId)
        {
            EventDetailsModel model = null;
            using(DAOEvent dao = new DAOEvent())
            {
                DTOEvent dtoEvent = dao.getEventById(eventId);
                if (dtoEvent == null)
                    return null;

                Mapper.Map(dtoEvent, model, dtoEvent.GetType(), model.GetType());
            }
            return model;
        }

        public static IList<EventOverviewModel> getEventsByUser(string userName)
        {
            List<EventOverviewModel> result = new List<EventOverviewModel>();
            using(DAOEvent dao = new DAOEvent())
            {
                IList<DTOEvent> events = dao.getEventsByUser(userName);
                if (events.Count == 0)
                    return result;

                foreach (DTOEvent dtoEvent in events)
                {
                    EventOverviewModel model = null;
                    Mapper.Map(dtoEvent, model, dtoEvent.GetType(), model.GetType());
                    result.Add(model);
                }
            }
            return result;
        }

        public static IList<AnswerModel> getAnswersByQuestion(int questionId)
        {
            List<AnswerModel> result = new List<AnswerModel>();
            using (DAOAnswer dao = new DAOAnswer())
            {
                IList<DTOAnswer> answers = dao.getAnswersByQuestion(questionId);
                if (answers.Count == 0)
                    return result;

                foreach (DTOAnswer dtoAnswer in answers)
                {
                    AnswerModel model = null;
                    Mapper.Map(dtoAnswer, model, dtoAnswer.GetType(), model.GetType());
                    result.Add(model);
                }
            }
            return result;
        }
    }
}