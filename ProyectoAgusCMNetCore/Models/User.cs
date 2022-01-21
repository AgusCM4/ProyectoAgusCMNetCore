﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int Userid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("user_code")]
        public string UserCode { get; set; }

        [Column("is_admin")]
        public bool Admin { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}