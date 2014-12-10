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
        public static bool SetAnswerCount(int eventId, int questionNumber, IList<string> answers)
        {
            EventDetailsModel eventDetails = null;
            QuestionModel questionDetails = null;
            AnswerModel currentAnswer = new AnswerModel();

            using (DAOEvent dao = new DAOEvent())
            {
                DTOEvent dtoEvent = dao.GetEventById(eventId);
                if (dtoEvent == null)
                    return false;

                eventDetails = Mapper.Map<DTOEvent, EventDetailsModel>(dtoEvent);
            }


            questionDetails = eventDetails.Questions.ElementAtOrDefault(questionNumber-1);

            if (questionDetails == null)
                return false;

            foreach(string ans in answers)
            {
                currentAnswer.Answer = ans;
                // contain probleem oplossen
                if (questionDetails.Answers.Contains<AnswerModel>(currentAnswer))
                {
                    using (DAOAnswer dao = new DAOAnswer())
                    {
                        dao.IncrementVotes(questionDetails.Answers.IndexOf(currentAnswer));
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