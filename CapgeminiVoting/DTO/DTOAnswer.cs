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
        public int id { get; set; }

        [Required]
        [Column("answer")]
        public string answer { get; set; }

        [Required]
        [Column("predefined")]
        public bool predefined { get; set; }

        [Required]
        [Column("votes")]
        public int votes { get; set; }

        [Required]
        [Column("question_id")]
        public int questionId { get; set; }

        [ForeignKey("questionId")]
        public virtual DTOQuestion question { get; set; }
    }
}
