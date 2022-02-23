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

            User usuariocreado = this.repo.FindUser(t.usuariocreado);
            User usuarioasignado = this.repo.FindUser(t.usuarioasignado);

            ViewData["USUARIOCREADO"] = usuariocreado;
            ViewData["USUARIOASIGNADO"] = usuarioasignado;
            ViewData["COMENTARIOS"] = this.repo.GetComments(idticket);

            return View(t);
        }

        [HttpPost]
        public IActionResult DetallesTicket(string messagetext, int idTicket)
        {
            this.repo.CreateComment(idTicket,messagetext,HttpContext.User.Identity.Name);

            return RedirectToAction("DetallesTicket", "Users", new { idTicket = idTicket });
        }

        public IActionResult AdminGrupos()
        {
            List<Group> grupos = this.repo.GetGroups();

            return View(grupos);
        }

        public IActionResult AdminUsuarios()
        {
            List<User> listadousers = this.repo.GetUsers();

            return View(listadousers);
        }

        public IActionResult AsignarmeTicket(int idticket)
        {
            this.repo.AsignMeTicket(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), idticket);

            return RedirectToAction("DetallesTicket", "Users", new { idticket = idticket });
        }

        public IActionResult MisTicketsAsignados()
        {
            int iduser = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            List<Ticket> tickets = this.repo.GetAssignedTickets(iduser);

            return View(tickets);
        }

        public IActionResult CompletarTicket(int idTicket)
        {
            this.repo.CompleteTicket(idTicket,HttpContext.User.Identity.Name);

            return RedirectToAction("MisTicketsAsignados", "Users");
        }

        public IActionResult PerfilUsuario(int? iduser)
        {
            if (iduser!=null)
            {
                User usu = this.repo.FindUser(iduser.Value);

                return View(usu);
            }
            else
            {
                User usu = this.repo.FindUser(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));

                return View(usu);
            }           
        }
        
        public IActionResult EditarPerfil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditarPerfil(string direccion, string phone, string profesion)
        {
            this.repo.EditUser(int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), direccion, phone, profesion);

            return RedirectToAction("PerfilUsuario", "Users");
        }

    }
}
