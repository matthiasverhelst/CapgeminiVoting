using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CapgeminiVoting.Models
{
    public class EventDetailsModel
    {
        [Required]
        [Display(Name = "Table_name", ResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Table_startdate", ResourceType = typeof(Resources))]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Table_enddate", ResourceType = typeof(Resources))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Table_description", ResourceType = typeof(Resources))]
        public string Description { get; set; }

        public IList<QuestionModel> Questions { get; set; }
    }
}