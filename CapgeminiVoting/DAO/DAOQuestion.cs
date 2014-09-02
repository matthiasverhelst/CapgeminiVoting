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
            IQueryable<DTOQuestion> query = from question in db.questions where question.eventId == eventId select question;
            return query.ToList();
        }
    }
}