using CapgeminiVoting.Contexts;
using CapgeminiVoting.DTO;
using CapgeminiVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.DAO
{
    public class DAOEvent : DAOBase<VotingContext>
    {
        public DTOEvent GetEventById(int eventId)
        {
            var query = from @event in db.Events
                        where @event.id == eventId
                        select @event;
            List<DTOEvent> result = query.ToList();
            return result.Count > 0 ? result.First() : null;
        }

        public IList<DTOEvent> GetEventsByUser(string userName)
        {
            IQueryable<DTOEvent> query = from @event in db.Events where @event.userName.Equals(userName) select @event;
            return query.ToList();
        }

        public bool CreateEvent(DTOEvent @event)
        {
            db.Events.Add(@event);
            int result = db.SaveChanges();
            return (result == 1);
        }
    }
}