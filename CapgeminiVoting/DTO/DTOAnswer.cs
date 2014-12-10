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
        public int Id { get; set; }

        [Required]
        [Column("answer")]
        public string Answer { get; set; }

        [Required]
        [Column("predefined")]
        public bool Predefined { get; set; }

        [Required]
        [Column("votes")]
        public int Votes { get; set; }

        [Required]
        [Column("question_id")]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual DTOQuestion Question { get; set; }
    }
}
