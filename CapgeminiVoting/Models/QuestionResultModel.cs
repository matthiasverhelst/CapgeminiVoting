﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class QuestionResultModel
    {
        public string Question { get; set; }

        public IList<AnswerResultModel> AnswerResult { get; set; }
    }
}