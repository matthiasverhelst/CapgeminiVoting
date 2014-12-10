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
    public class MobileBusinessLayer
    {
        public static bool SetAnswerCount(int eventId, int questionNumber, IList<AnswerModel> answers)
        {
            EventDetailsModel eventDetails = null;
            QuestionModel questionDetails = null;

            using (DAOEvent dao = new DAOEvent())
            {
                DTOEvent dtoEvent = dao.GetEventById(eventId);
                if (dtoEvent == null)
                    return false;

                eventDetails = Mapper.Map<DTOEvent, EventDetailsModel>(dtoEvent);
            }


            questionDetails = eventDetails.Questions.ElementAtOrDefault(questionNumber);

            if (questionDetails == null)
                return false;

            foreach(AnswerModel ans in answers)
            {
                if (questionDetails.Answers.Contains(ans))
                {
                    using (DAOAnswer dao = new DAOAnswer())
                    {
                        dao.IncrementVotes(questionDetails.Answers.IndexOf(ans));
                    }
                }
            }

            // if it does not, get questionID based on eventcode

            // and create this answer knowing: 
            // - answer
            // - predefined = false
            // - votes = 0
            // - questionID = ?

            // if it does, update answer

            return true;
        }
    }
}