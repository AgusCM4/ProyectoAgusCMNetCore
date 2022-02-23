using Microsoft.AspNetCore.Mvc;
using ProyectoAgusCMNetCore.Models;
using ProyectoAgusCMNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    public class SoporteController : Controller
    {
        private RepositoryApp repo;

        public SoporteController(RepositoryApp repo)
        {
            this.repo = repo;
        }

        public IActionResult ConsultaTickets()
        {
            List<Ticket> ticketsoporte = this.repo.GetTicketsSoporte();

            return View(ticketsoporte);
        }
    }
}
