using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Models
{
    [Table("profile")]
    public class Profile
    {
        [Key]
        [Column("profile_id")]
        public int profile_id { get; set; }

        [Column("day_of_birth")]
        public DateTime DayOfBirth { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("bio")]
        public string bio { get; set; }

        [Column("likes")]
        public int likes { get; set; }

        [Column("dislikes")]
        public int dislikes { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("profile_pic")]
        public string ProfilePic { get; set; }
    }
}
