using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Models
{
    [Table("ticket")]
    public class Ticket
    {
        [Key]
        [Column("ticket_id")]
        public int idticket { get; set; }

        [Column("title")]
        public string titulo { get; set; }

        [Column("ticket_text")]
        public string textoticket { get; set; }

        [Column("user_create")]
        public int usuariocreado { get; set; }

        [Column("user_assigned")]
        public int usuarioasignado { get; set; }

        [Column("group_assigned")]
        public int grupoasingado { get; set; }

        [Column("created_at")]
        public DateTime fechacreacion { get; set; }

        [Column("ticket_status")]
        public string estadoticket { get; set; }
    }
}
