using Microsoft.AspNetCore.Mvc;
using ProyectoAgusCMNetCore.Models;
using ProyectoAgusCMNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    public class DesarrolladoresController : Controller
    {
        private RepositoryApp repo;

        public DesarrolladoresController(RepositoryApp repo)
        {
            this.repo = repo;
        }

        public IActionResult ConsultaTickets()
        {
            List<Ticket> ticketsdesarrollo = this.repo.GetTicketsDesarrollo();
            return View(ticketsdesarrollo);
        }
    }
}
