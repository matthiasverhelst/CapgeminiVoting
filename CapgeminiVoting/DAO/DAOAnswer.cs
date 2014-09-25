using CapgeminiVoting.Contexts;
using CapgeminiVoting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.DAO
{
    public class DAOAnswer : DAOBase<VotingContext>
    {
        public IList<DTOAnswer> getAnswersByQuestion(int questionId)
        {
            IQueryable<DTOAnswer> query = from answer in db.Answers where answer.questionId == questionId select answer;
            return query.ToList();
        }
    }
}