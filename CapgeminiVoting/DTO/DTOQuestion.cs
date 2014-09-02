using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CapgeminiVoting.DTO
{
    [Table("Question")]
    public class DTOQuestion
    {
        [Key]
        [Required]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Column("question")]
        public string question { get; set; }

        [Required]
        [Column("question_type")]
        public byte questionType { get; set; }

        [Required]
        [Column("event_id")]
        public int eventId { get; set; }

        [ForeignKey("eventId")]
        public virtual DTOEvent @event { get; set; }

        public virtual IList<DTOAnswer> answers { get; set; }
    }
}
