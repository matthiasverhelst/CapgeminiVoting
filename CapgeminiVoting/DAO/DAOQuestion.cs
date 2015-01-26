using CapgeminiVoting.Contexts;
using CapgeminiVoting.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.DAO
{
    public class DAOQuestion : DAOBase<VotingContext>
    {
        public DTOQuestion GetQuestionById(int id)
        {
            var query = (from question in db.Questions
                        where question.Id == id
                        select question).AsNoTracking();
            List<DTOQuestion> result = query.ToList();
            return result.Count > 0 ? result.First() : null;
        }

        public IList<DTOQuestion> GetQuestionsByEvent(int eventId)
        {
            var query = from question in db.Questions 
                        where question.EventId == eventId 
                        orderby question.QuestionNumber
                        select question;
            return query.ToList();
        }
    }
}