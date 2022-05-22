using Microsoft.AspNetCore.Mvc;
using NugetProyectoAgus;
using ProyectoAgusCMNetCore.Filters;
using ProyectoAgusCMNetCore.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    [AuthorizeUsers]
    public class UsersController : Controller
    {
        private ServiceApiProyecto service;

        public UsersController(ServiceApiProyecto service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> NuevaPeticion()
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            List<NugetProyectoAgus.Group> grupos = await this.service.GetGroups(token);
            ViewData["GRUPOS"] = grupos;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevaPeticion(string titulo, string tickettext, int grupo, int idusuariocreado)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.SaveTicket(token, titulo, tickettext, idusuariocreado, grupo);

            User creador = await this.service.FindUser(token, idusuariocreado);
            await this.service.SendMail("agustincampostajamar@gmail.com","Nueva Petición " + titulo, tickettext + " Usuario Creado: "+creador.Name);
            return RedirectToAction("Index");
        }

        public IActionResult NuevoGrupo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevoGrupo(string nombre)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.CreateGroup(token,nombre);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> TicketsActivosCreados()
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            int iduser = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<Ticket> tickets = await this.service.GetCreatedTickets(token,iduser);
            return View(tickets);
        }

        public async Task<IActionResult> DetallesTicket(int idticket)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            Ticket t = await this.service.FindTicket(token,idticket);
            User usuariocreado = await this.service.FindUser(token,t.usuariocreado);
            User usuarioasignado = await this.service.FindUser(token, t.usuarioasignado);

            ViewData["USUARIOCREADO"] = usuariocreado;
            ViewData["USUARIOASIGNADO"] = usuarioasignado;
            ViewData["COMENTARIOS"] = await this.service.GetComments(token,idticket);

            return View(t);
        }

        [HttpPost]
        public async Task<IActionResult> DetallesTicket(string messagetext, int idTicket)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.CreateComment(token,idTicket,messagetext,HttpContext.User.Identity.Name);
            return RedirectToAction("DetallesTicket", "Users", new { idTicket = idTicket });
        }

        public async Task<IActionResult> AdminGrupos()
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            List<NugetProyectoAgus.Group> grupos = await this.service.GetGroups(token);
            return View(grupos);
        }

        public async Task<IActionResult> AdminUsuarios()
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            List<User> listadousers = await this.service.GetUsers(token);
            return View(listadousers);
        }

        public async Task<IActionResult> AsignarmeTicket(int idticket)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.AssignMeTicket(token,int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), idticket);
            return RedirectToAction("DetallesTicket", "Users", new { idticket = idticket });
        }

        public async Task<IActionResult> MisTicketsAsignados()
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            int iduser = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<Ticket> tickets = await this.service.GetAssignedTickets(token,iduser);
            return View(tickets);
        }

        public async Task<IActionResult> CompletarTicket(int idTicket)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.CompleteTicket(token,idTicket,HttpContext.User.Identity.Name);

            Ticket t = await this.service.FindTicket(token, idTicket);

            User usercreated = await this.service.FindUser(token, t.usuariocreado);

            await this.service.SendMail(usercreated.Email, "PETICION REALIZADA " + t.idticket, t.textoticket);

            return RedirectToAction("MisTicketsAsignados", "Users");
        }

        public async Task<IActionResult> PerfilUsuario(int? iduser)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            if (iduser!=null)
            {
                User usu = await this.service.FindUser(token,iduser.Value);
                return View(usu);
            }
            else
            {
                User usu = await this.service.FindUser(token,int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
                return View(usu);
            }           
        }
        
        public IActionResult EditarPerfil()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditarPerfil(string direccion, string phone, string profesion)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.EditUser(token,int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), direccion, phone, profesion);
            return RedirectToAction("PerfilUsuario", "Users");
        }

    }
}
