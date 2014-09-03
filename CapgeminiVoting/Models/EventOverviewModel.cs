using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class EventOverviewModel
    {
        [Display(Name = Resources.Table_name)]
        public string name { get; set; }

        [Display(Name = Resources.Table_code)]
        public int code { get; set; }

        [Display(Name = Resources.Table_startdate)]
        public DateTime startDate { get; set; }

        [Display(Name = Resources.Table_enddate)]
        public DateTime endDate { get; set; }

        [Display(Name = Resources.Table_description)]
        public string description { get; set; }
    }
}