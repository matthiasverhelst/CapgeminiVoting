using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CapgeminiVoting.Models
{
    public class EventDetailsModel
    {
        [Required]
        public string name { get; set; }

        [Required]
        public DateTime startDate { get; set; }

        [Required]
        public DateTime endDate { get; set; }

        public string description { get; set; }

        public IList<QuestionModel> questions { get; set; }
    }
}