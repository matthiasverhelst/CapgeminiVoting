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

            foreach(string ans in answers)
            {
                for (int i = 0; i < questionDetails.Answers.Count(); i++ )
                {
                    if (questionDetails.Answers[i].Answer.Equals(ans))
                    {
                        using (DAOAnswer dao = new DAOAnswer())
                        {
                            dao.IncrementVotes(questionDetails.Answers[i].Id);
                        }
                    }
                    else if (questionDetails.QuestionType == 2) // Free text answer
                    {
                        // Update nog meerdere keren, moet aangepast worden voor maar 1 keer
                        using (DAOAnswer dao = new DAOAnswer())
                        {
                            DTOAnswer newAnswer = new DTOAnswer();
                            newAnswer.Answer = ans;
                            newAnswer.Predefined = false;
                            newAnswer.QuestionId = questionDetails.Id;
                            newAnswer.Votes = 0;
                            dao.CreateAnswer(newAnswer);
                        }
                    }

                }
            }
            return true;
        }
    }
}