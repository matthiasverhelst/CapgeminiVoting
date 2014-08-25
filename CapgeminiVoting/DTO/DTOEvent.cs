using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CapgeminiVoting.DTO
{
    public class DTOEvent
    {
        [Key]
        [Required]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Range(100000, 999999)]
        [Index(IsUnique = true)]
        [Column("code")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int code { get; set; }

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
        [Column("user_id")]
        public int userId { get; set; }

        [ForeignKey("userId")]
        public virtual DTOUser user { get; set; }
    }
}