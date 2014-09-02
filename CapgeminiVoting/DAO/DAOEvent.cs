using CapgeminiVoting.Contexts;
using CapgeminiVoting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.DAO
{
    public class DAOEvent : DAOBase<VotingContext>
    {
        public DTOEvent getEventById(int eventId)
        {
            IQueryable<DTOEvent> query = from @event in db.events where @event.id == eventId select @event;
            List<DTOEvent> result = query.ToList();
            return result.Count > 0 ? result.First() : null;
        }

        public IList<DTOEvent> getEventsByUser(string userName)
        {
            IQueryable<DTOEvent> query = from @event in db.events where @event.userName == userName select @event;
            return query.ToList();
        }
    }
}