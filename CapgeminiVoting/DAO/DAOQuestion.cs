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
        public IList<DTOQuestion> getQuestionsByEvent(int eventId)
        {
            var query = from question in db.Questions 
                        where question.eventId == eventId 
                        orderby question.questionNumber
                        select question;
            return query.ToList();
        }
    }
}