using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Models
{
    [Table("country")]
    public class Country
    {
        [Key]
        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("country_name")]
        public string CountryName { get; set; }
    }
}
