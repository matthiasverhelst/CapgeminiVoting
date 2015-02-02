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
            IList<DTOQuestion> questions = null;
            DTOQuestion currQuestion = null;
            bool answerFound = false;

            using (DAOQuestion daoQuestion = new DAOQuestion())
            {
                questions = daoQuestion.GetQuestionsByEvent(eventId);
                if (questions.Count == 0)
                    return false;

                for (int i = 0; i < questions.Count(); i++ )
                    if (questions[i].QuestionNumber == questionNumber)
                        currQuestion = questions[i];

                if (currQuestion == null)
                    return false;

                daoQuestion.IncrementVoterCount(currQuestion.Id);

                foreach (string ans in answers)
                    for (int i = 0; i < currQuestion.Answers.Count(); i++)
                        if (currQuestion.Answers[i].Answer.Equals(ans))
                        {
                            using (DAOAnswer daoAnswer = new DAOAnswer())
                            {
                                daoAnswer.IncrementVotes(currQuestion.Answers[i].Id);
                                answerFound = true;
                            }
                        }
            }

            if (!answerFound)
                if (currQuestion.QuestionType == 2)
                {
                    using (DAOAnswer daoAnswer = new DAOAnswer())
                    {
                        DTOAnswer newAnswer = new DTOAnswer();
                        newAnswer.Answer = answers.First().ToString();
                        newAnswer.Predefined = false;
                        newAnswer.QuestionId = currQuestion.Id;
                        newAnswer.Votes = 1;
                        daoAnswer.CreateAnswer(newAnswer);
                    }
                }
                else
                    return false;
            return true;
        }

        public static bool IsEventLocked(int id)
        {
            using (var daoEvent = new DAOEvent())
            {
                var @event = daoEvent.GetEventById(id);
                if (@event != null)
                    return (@event.Locked == true);
                return true;
            }
        }
    }
}