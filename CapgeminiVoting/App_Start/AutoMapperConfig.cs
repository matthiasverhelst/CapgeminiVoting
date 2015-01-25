using AutoMapper;
using CapgeminiVoting.DTO;
using CapgeminiVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<DTOQuestion, QuestionModel>();
            Mapper.CreateMap<DTOEvent, EventDetailsModel>();
            Mapper.CreateMap<DTOEvent, EventOverviewModel>();
            Mapper.CreateMap<DTOAnswer, AnswerModel>();
            Mapper.CreateMap<EventDetailsModel, DTOEvent>();
            Mapper.CreateMap<QuestionModel, DTOQuestion>();
            Mapper.CreateMap<AnswerModel, DTOAnswer>();
            Mapper.CreateMap<DTOAnswer, AnswerResultModel>();
        }
    }
}