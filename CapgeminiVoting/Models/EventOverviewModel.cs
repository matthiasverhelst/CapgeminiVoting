using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class EventOverviewModel
    {
        public int id { get; set; }

        [Display(Name = "Table_name", ResourceType = typeof(Resources))]
        public string name { get; set; }

        [Display(Name = "Table_code", ResourceType = typeof(Resources))]
        public int code { get; set; }

        [Display(Name = "Table_startdate", ResourceType = typeof(Resources))]
        public DateTime startDate { get; set; }

        [Display(Name = "Table_enddate", ResourceType = typeof(Resources))]
        public DateTime endDate { get; set; }

        [Display(Name = "Table_description", ResourceType = typeof(Resources))]
        public string description { get; set; }
    }
}