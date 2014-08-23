using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class QuestionModel
    {
        public string question { get; set; }
        public int questionType { get; set; }
        public List<AnswerModel> answers { get; set; }
    }
}