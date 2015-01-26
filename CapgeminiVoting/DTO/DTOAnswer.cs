using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CapgeminiVoting.DTO
{
    [Table("Answer")]
    public class DTOAnswer
    {
        [Key]
        [Required]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        [Column("answer")]
        public virtual string Answer { get; set; }

        [Required]
        [Column("predefined")]
        public virtual bool Predefined { get; set; }

        [Required]
        [Column("votes")]
        public virtual int Votes { get; set; }

        [Required]
        [Column("question_id")]
        public virtual int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual DTOQuestion Question { get; set; }
    }
}
