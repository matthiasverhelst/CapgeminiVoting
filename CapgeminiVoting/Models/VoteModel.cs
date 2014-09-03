using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.Models
{
    public class VoteModel
    {
        [Required(ErrorMessage = "")]
        public string code { get; set; }
    }
}