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
        public static bool SetAnswerCount(int eventId, int questionNumber, IList<int> answerIds, string freeTextAnswer)
        {
            IList<DTOQuestion> questions = null;
            DTOQuestion currQuestion = null;

            using (DAOQuestion daoQuestion = new DAOQuestion())
            {
                questions = daoQuestion.GetQuestionsByEvent(eventId);
                if (questions.Count == 0)
                    return false;

                for (int i = 0; i < questions.Count(); i++)
                    if (questions[i].QuestionNumber == questionNumber)
                        currQuestion = questions[i];

                if (currQuestion == null)
                    return false;

                daoQuestion.IncrementVoterCount(currQuestion.Id);
            }

            switch (currQuestion.QuestionType)
            {
                case 0:
                case 1:
                    if (answerIds != null)
                        foreach (int ans in answerIds)
                            using (DAOAnswer daoAnswer = new DAOAnswer())
                            {
                                daoAnswer.IncrementVotes(ans);
                            }
                    else
                        return false;
                    break;
                case 2:
                    if (freeTextAnswer != null)
                        using (DAOAnswer daoAnswer = new DAOAnswer())
                        {
                            DTOAnswer newAnswer = new DTOAnswer();
                            newAnswer.Answer = freeTextAnswer;
                            newAnswer.Predefined = false;
                            newAnswer.QuestionId = currQuestion.Id;
                            newAnswer.Votes = 1;
                            daoAnswer.CreateAnswer(newAnswer);
                        }
                    else
                        return false;
                    break;
                default:
                    return false;
            }
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