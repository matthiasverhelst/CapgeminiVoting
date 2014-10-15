﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class EventOverviewModel
    {
        [Display(Name = "Table_code", ResourceType = typeof(Resources))]
        public int Id { get; set; }

        [Display(Name = "Table_name", ResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Display(Name = "Table_startdate", ResourceType = typeof(Resources))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Table_enddate", ResourceType = typeof(Resources))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Table_description", ResourceType = typeof(Resources))]
        public string Description { get; set; }
    }
}