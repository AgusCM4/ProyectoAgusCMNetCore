using Microsoft.AspNetCore.Mvc;
using NugetProyectoAgus;
using ProyectoAgusCMNetCore.Models;
using ProyectoAgusCMNetCore.Repositories;
using ProyectoAgusCMNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    public class DesarrolladoresController : Controller
    {
        private ServiceApiProyecto service;

        public DesarrolladoresController(ServiceApiProyecto service)
        {
            this.service = service;
        }

        public async Task<IActionResult> ConsultaTickets()
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            List<Ticket> ticketsdesarrollo = await this.service.GetTicketsDesarrollo(token);
            return View(ticketsdesarrollo);
        }
    }
}
