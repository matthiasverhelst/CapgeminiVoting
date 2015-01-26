using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class QuestionModel : IComparable<QuestionModel>
    {
        public int Id { get; set; }

        [Display(Name = "Question", ResourceType = typeof(Resources))]
        public string Question { get; set; }

        [Display(Name = "Type", ResourceType = typeof(Resources))]
        public int QuestionType { get; set; }

        public int QuestionNumber { get; set; }

        [Display(Name = "Possible_answers", ResourceType = typeof(Resources))]
        public IList<AnswerModel> Answers { get; set; }
    
        int IComparable<QuestionModel>.CompareTo(QuestionModel other)
        {
            if (this.QuestionNumber < other.QuestionNumber)
                return -1;
            else if (this.QuestionNumber > other.QuestionNumber)
                return 1;
            else return 0;
        }
    }
}