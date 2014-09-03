using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class EventOverviewModel
    {
        [Display(Name="Event name")]
        public string name { get; set; }

        [Display(Name = "Start date")]
        public DateTime startDate { get; set; }

        [Display(Name = "End date")]
        public DateTime endDate { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }
    }
}