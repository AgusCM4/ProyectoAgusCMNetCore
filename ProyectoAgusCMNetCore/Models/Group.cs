using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Models
{
    [Table("group")]
    public class Group
    {
        [Key]
        [Column("id_group")]
        public string idgroup { get; set; }

        [Column("name")]
        public string name { get; set; }
    }
}
