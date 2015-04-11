using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class QuestionResultModel
    {
        public string Question { get; set; }

        public int VoterCount { get; set; }

        public IDictionary<string, int> AnswerResult { get; set; }
    }
}