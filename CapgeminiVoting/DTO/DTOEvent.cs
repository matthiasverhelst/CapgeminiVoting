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
        public virtual int Id { get; set; }

        [Required]
        [Column("name")]
        public virtual string Name { get; set; }

        [Required]
        [Column("start_date")]
        public virtual DateTime CreationDate { get; set; }

        [Column("description")]
        public virtual string Description { get; set; }

        [Column("locked")]
        public virtual bool Locked { get; set; }

        [Required]
        [Column("user_name")]
        public virtual string UserName { get; set; }

        [ForeignKey("UserName")]
        public virtual ApplicationUser User { get; set; }

        public virtual IList<DTOQuestion> Questions { get; set; }
    }
}