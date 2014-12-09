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
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("start_date")]
        public DateTime CreationDate { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("user_name")]
        public string UserName { get; set; }

        [ForeignKey("UserName")]
        public virtual ApplicationUser User { get; set; }

        public virtual IList<DTOQuestion> Questions { get; set; }
    }
}