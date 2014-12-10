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
            var query = from @event in db.Events
                        where @event.Id == eventId
                        select @event;
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
            var originalEvent = db.Events.Find(@event.Id);
            db.Entry(originalEvent).OriginalValues.SetValues(originalEvent);
            db.Entry(originalEvent).CurrentValues.SetValues(@event);

            originalEvent.Questions = @event.Questions;

            db.Entry(originalEvent).State = EntityState.Modified;

            var result = db.SaveChanges();

            return result >= 0;
        }
        public bool DeleteEventById(int eventId)
        {
            var query = (from @event in db.Events
                        where @event.id == eventId
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