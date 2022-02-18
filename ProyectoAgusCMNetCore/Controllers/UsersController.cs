using Microsoft.AspNetCore.Mvc;
using ProyectoAgusCMNetCore.Filters;
using ProyectoAgusCMNetCore.Models;
using ProyectoAgusCMNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    [AuthorizeUsers]
    public class UsersController : Controller
    {
        private RepositoryApp repo;

        public UsersController(RepositoryApp repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NuevaPeticion()
        {
            List<Group> grupos = this.repo.GetGroups();

            ViewData["GRUPOS"] = grupos;
            return View();
        }

        [HttpPost]
        public IActionResult NuevaPeticion(string titulo, string tickettext, int grupo, int idusuariocreado)
        {
            this.repo.SaveTicket(titulo, tickettext, idusuariocreado, grupo);
            
            return RedirectToAction("Index");
        }

        public IActionResult NuevoGrupo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NuevoGrupo(string nombre)
        {
            this.repo.CreateGroup(nombre);

            return RedirectToAction("Index");
        }


        public IActionResult TicketsActivosCreados()
        {
            int iduser = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            List<Ticket> tickets = this.repo.GetCreatedTickets(iduser);

            return View(tickets);
        }

        public IActionResult DetallesTicket(int idticket)
        {
            Ticket t = this.repo.FindTicket(idticket);
            return View(t);
        }


    }
}
