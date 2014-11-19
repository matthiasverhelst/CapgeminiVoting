using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class QuestionModel
    {
        public string Question { get; set; }

        public int QuestionType { get; set; }

        public int QuestionNumber { get; set; }

        public IList<AnswerModel> Answers { get; set; }
    }
}