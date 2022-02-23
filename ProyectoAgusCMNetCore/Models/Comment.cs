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
        public int IdComentario { get; set; }

        [Column("ticket_id")]
        public int idTicket { get; set; }

        [Column("comment_text")]
        public string Comentario { get; set; }

        [Column("comment_date")]
        public DateTime FechaComentario { get; set; }

        [Column("comment_user")]
        public string ComentarioUsuario { get; set; }
    }
}
