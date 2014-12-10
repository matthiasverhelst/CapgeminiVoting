using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class VoteResultModel
    {
        [Required(ErrorMessage = "")]
        public string EventCode { get; set; }

        public int QuestionNumber { get; set; }

        public IList<AnswerModel> Answer { get; set; }
    }
}