using CapgeminiVoting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.DTO
{
    [Table("Event")]
    public class DTOEvent
    {
        [Key]
        [Required]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Column("name")]
        public string name { get; set; }

        [Required]
        [Column("start_date")]
        public DateTime startDate { get; set; }

        [Required]
        [Column("end_date")]
        public DateTime endDate { get; set; }

        [Column("description")]
        public string description { get; set; }

        [Required]
        [Column("user_name")]
        public string userName { get; set; }

        [ForeignKey("userName")]
        public virtual ApplicationUser user { get; set; }

        public virtual IList<DTOQuestion> questions { get; set; }
    }
}