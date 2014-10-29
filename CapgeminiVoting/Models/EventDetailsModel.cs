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
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public IList<QuestionModel> Questions { get; set; }
    }
}