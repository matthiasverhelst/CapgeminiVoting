using AutoMapper;
using CapgeminiVoting.DAO;
using CapgeminiVoting.DTO;
using CapgeminiVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.BusinessLayer
{
    public class AdminBusinessLayer
    {
        public static EventModel getEventById(int eventId)
        {
            EventModel model;
            using(DAOEvent dao = new DAOEvent())
            {
                DTOEvent dtoEvent = dao.getEventById(eventId);
                if (dtoEvent == null)
                    return null;

                Mapper.CreateMap<DTOEvent, EventModel>();
                model = Mapper.Map<EventModel>(dtoEvent);
            }
            return model;
        }
    }
}