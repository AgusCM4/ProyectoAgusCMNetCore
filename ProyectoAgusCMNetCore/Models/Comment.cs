using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Models
{
    [Table("comment")]
    public class Comment
    {
        [Key]
        [Column("comment_id")]
        public int CommentId { get; set; }

        [Column("comment_text")]
        public string CommentText { get; set; }

        [Column("rating")]
        public double Rating { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }
    }
}
