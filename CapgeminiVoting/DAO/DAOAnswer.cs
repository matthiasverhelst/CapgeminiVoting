using CapgeminiVoting.Contexts;
using CapgeminiVoting.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.DAO
{
    public class DAOAnswer : DAOBase<VotingContext>
    {
        public DTOAnswer GetAnswerById(int id)
        {
            var query = (from answer in db.Answers
                        where answer.Id == id
                        select answer).AsNoTracking();
            List<DTOAnswer> result = query.ToList();
            return result.Count > 0 ? result.First() : null;
        }

        public IList<DTOAnswer> GetAnswersByQuestion(int questionId)
        {
            var query = from answer in db.Answers where answer.QuestionId == questionId select answer;
            return query.ToList();
        }

        public bool IncrementVotes(int answerId)
        {
            var query = (from answer in db.Answers
                         where answer.Id == answerId
                         select answer).First();

            if (query == null)
                return false;
            else
            {
                query.Votes++;
                int result = db.SaveChanges();
                return (result == 1);
            }
        }

        public bool CreateAnswer(DTOAnswer answer)
        {
            db.Answers.Add(answer);
            int result = db.SaveChanges();
            return (result == 1);
        }

        public int GetFreeTextAnswerId(int questionId, string answer)
        {
            IList<DTOAnswer> dtoAnswers = GetAnswersByQuestion(questionId);
            foreach (var dtoAnswer in dtoAnswers)
            {
                if (dtoAnswer.Answer.Trim().Equals(answer))
                    return dtoAnswer.Id;
            }

            return -1;
        }
    }
}