using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Models
{
    [Table("post")]
    public class Post
    {
        [Key]
        [Column("post_id")]
        public int PostId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("content_id")]
        public int ContentId { get; set; }

        [Column("content_type")]
        public int ContentType { get; set; }

        [Column("content_public")]
        public bool ContentPublic { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("media_rating")]
        public double MediaRating { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

    }
}
