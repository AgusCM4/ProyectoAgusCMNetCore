﻿using Microsoft.EntityFrameworkCore;
using ProyectoAgusCMNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Data
{
    public class MainContext:DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
