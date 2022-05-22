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
    public class AdminController : Controller
    {
        private ServiceApiProyecto service;

        public AdminController(ServiceApiProyecto service)
        {
            this.service = service;
        }

        public async Task<IActionResult> UsuariosPorGrupo(string idGrupo)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            List<User> users = await this.service.GetUsersGroup(token,idGrupo);

            string nombregrupo = await this.service.GetNameGroup(token,idGrupo);
            ViewData["GRUPO"] = nombregrupo;            

            return View(users);
        }

        public async Task<IActionResult> EliminarUsuarios(int idUser)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.DeleteUser(token,idUser);
            return RedirectToAction("AdminUsuarios","Users");
        }

        public async Task<IActionResult> EliminarGrupos(string idgrupo)
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            await this.service.DeleteGroup(token, idgrupo);
            return RedirectToAction("AdminGrupos", "Users");
        }

        public async Task<IActionResult> VistaTickets()
        {
            string token = HttpContext.User.FindFirst("TOKEN").Value;
            List<Ticket> tickets = await this.service.GetTickets(token);
            return View(tickets);
        }
    }
}
