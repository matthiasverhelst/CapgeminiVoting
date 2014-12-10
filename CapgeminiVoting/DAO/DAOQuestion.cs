using CapgeminiVoting.Contexts;
using CapgeminiVoting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.DAO
{
    public class DAOQuestion : DAOBase<VotingContext>
    {
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