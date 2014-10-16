using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class VoteModel
    {
        [Required(ErrorMessage = "")]
        public string EventCode { get; set; }

        public int QuestionIndex { get; set; }
    }
}