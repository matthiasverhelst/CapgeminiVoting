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

            using (DAOEvent dao = new DAOEvent())
            {
                DTOEvent dtoEvent = dao.GetEventById(eventId);
                if (dtoEvent == null)
                    return false;

                eventDetails = Mapper.Map<DTOEvent, EventDetailsModel>(dtoEvent);
            }

            for (int i = 0; i < eventDetails.Questions.Count(); i++ )
            {
                if (eventDetails.Questions[i].QuestionNumber == questionNumber)
                {
                    questionDetails = eventDetails.Questions[i];
                }
            }

            if (questionDetails == null)
                return false;


            bool answerFound = false;

            foreach(string ans in answers)
            {
                for (int i = 0; i < questionDetails.Answers.Count(); i++ )
                {
                    if (questionDetails.Answers[i].Answer.Equals(ans))
                    {
                        using (DAOAnswer dao = new DAOAnswer())
                        {
                            dao.IncrementVotes(questionDetails.Answers[i].Id);
                            answerFound = true;
                        }
                    }
                }
            }

            if (!answerFound)
            {
                if (questionDetails.QuestionType == 2)
                {
                    using (DAOAnswer dao = new DAOAnswer())
                    {
                        DTOAnswer newAnswer = new DTOAnswer();
                        newAnswer.Answer = answers.First().ToString();
                        newAnswer.Predefined = false;
                        newAnswer.QuestionId = questionDetails.Id;
                        newAnswer.Votes = 1;
                        dao.CreateAnswer(newAnswer);
                    }
                }
                else
                    return false;
            }
            return true;
        }
    }
}