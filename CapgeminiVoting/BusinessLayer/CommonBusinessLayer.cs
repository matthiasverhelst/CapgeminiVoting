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
    public class CommonBusinessLayer
    {
        public static IList<QuestionModel> getQuestionsByEvent(int eventId)
        {
            List<QuestionModel> result = new List<QuestionModel>();
            using (DAOQuestion dao = new DAOQuestion())
            {
                IList<DTOQuestion> questions = dao.getQuestionsByEvent(eventId);
                if (questions.Count == 0)
                    return result;

                foreach (DTOQuestion dtoQuestion in questions)
                {
                    QuestionModel model = Mapper.Map<DTOQuestion,QuestionModel>(dtoQuestion);
                    result.Add(model);
                }
            }
            return result;
        }
    }
}