using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class QuestionInfoModel
    {
        [Required(ErrorMessage = "")]
        public string EventCode { get; set; }

        public QuestionModel QuestionData { get; set; }
    }
}