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
    public class MobileBusinessLayer
    {
        public static bool SetAnswerCount(int eventCode, int questionNumber)
        {

            // check if answer exists for this question
            // daoEvent.GetEventById

            // if it does not, get questionID based on eventcode

            // and create this answer knowing: 
            // - answer
            // - predefined = false
            // - votes = 0
            // - questionID = ?

            // if it does, update answer

            return true;
        }
    }
}