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
        public int Id { get; set; }

        [Required]
        [Column("question")]
        public string Question { get; set; }

        [Required]
        [Column("question_type")]
        public byte QuestionType { get; set; }

        [Required]
        [Column("question_number")]
        public int QuestionNumber { get; set; }

        [Required]
        [Column("event_id")]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual DTOEvent @Event { get; set; }

        public virtual IList<DTOAnswer> Answers { get; set; }
    }
}
