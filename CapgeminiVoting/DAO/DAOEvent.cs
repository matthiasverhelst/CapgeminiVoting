using CapgeminiVoting.Contexts;
using CapgeminiVoting.DTO;
using CapgeminiVoting.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.DAO
{
    public class DAOEvent : DAOBase<VotingContext>
    {
        public DTOEvent GetEventById(int eventId)
        {
            var query = (from @event in db.Events
                        where @event.Id == eventId
                        select @event).AsNoTracking();
            List<DTOEvent> result = query.ToList();
            return result.Count > 0 ? result.First() : null;
        }

        public IList<DTOEvent> GetEventsByUser(string userName)
        {
            IQueryable<DTOEvent> query = from @event in db.Events where @event.UserName.Equals(userName) select @event;
            return query.ToList();
        }

        public bool CreateEvent(DTOEvent @event)
        {
            db.Events.Add(@event);
            int result = db.SaveChanges();
            return (result > 0);
        }

        public bool ModifyEvent(DTOEvent @event)
        {
            //step 1: gather ids of already existing questions and answers related to this event
            var originalEvent = GetEventById(@event.Id);
            var existingQuestionIds = new List<int>();
            var existingAnswerIds = new List<int>();

            foreach (var question in originalEvent.Questions)
            {
                existingQuestionIds.Add(question.Id);

                foreach (var answer in question.Answers)
                {
                    existingAnswerIds.Add(answer.Id);
                }
            }

            //step 2: set the correct state to the updated event questions and answers; also remove these from the list created in step 1
            foreach (var question in @event.Questions)
            {
                foreach (var answer in question.Answers)
                {
                    if (answer.Id == 0)
                        db.Entry(answer).State = EntityState.Added;
                    else
                    {
                        db.Entry(answer).State = EntityState.Modified;
                        existingAnswerIds.Remove(answer.Id);
                    }
                }

                question.Answers = null;

                if (question.Id == 0)
                    db.Entry(question).State = EntityState.Added;
                else
                {
                    db.Entry(question).State = EntityState.Modified;
                    existingQuestionIds.Remove(question.Id);
                }
            }

            //step 3: for all ids not removed from the existing question or answer list: delete question or answer
            foreach (var id in existingQuestionIds)
            {
                using(var dao = new DAOQuestion())
                {
                    var question = dao.GetQuestionById(id);
                    question.Answers = null;
                    db.Entry(question).State = EntityState.Deleted;
                }
            }

            foreach (var id in existingAnswerIds)
            {
                using (var dao = new DAOAnswer())
                {
                    var answer = dao.GetAnswerById(id);
                    db.Entry(answer).State = EntityState.Deleted;
                }
            }

            //step 4: set the state of the event entity to modified
            @event.Questions = null;
            db.Entry(@event).State = EntityState.Modified;

            var result = db.SaveChanges();

            return result >= 0;
        }

        public bool DeleteEventById(int eventId)
        {
            var query = (from @event in db.Events
                        where @event.Id == eventId
                        select @event).First();
              //foreach (var ev in query)
                //{
                    //db.Events.DeleteOnSubmit(query);
                    db.Events.Remove(query);
                //}
              //try
              //{
                //  db.SubmitChanges();
                    int result = db.SaveChanges();
                    return (result == 1);
              //}
              //catch (Exception e)
              //{
                //  Console.WriteLine(e);
                  // Provide for exceptions.
              //}

         }
    }
}