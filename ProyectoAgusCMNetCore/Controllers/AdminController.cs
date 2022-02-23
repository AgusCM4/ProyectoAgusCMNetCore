using Microsoft.AspNetCore.Mvc;
using ProyectoAgusCMNetCore.Models;
using ProyectoAgusCMNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    public class AdminController : Controller
    {
        private RepositoryApp repo;

        public AdminController(RepositoryApp repo)
        {
            this.repo = repo;
        }

        public IActionResult UsuariosPorGrupo(string idGrupo)
        {
            List<User> users = this.repo.GetUsersGroup(idGrupo);

            string nombregrupo = this.repo.GetNameGroup(idGrupo);
            ViewData["GRUPO"] = nombregrupo;            

            return View(users);
        }

        public IActionResult EliminarUsuarios(int idUser)
        {
            this.repo.DeleteUser(idUser);

            return RedirectToAction("AdminUsuarios","Users");
        }

        public IActionResult EliminarGrupos(string idgrupo)
        {
            this.repo.DeleteGroup(idgrupo);

            return RedirectToAction("AdminGrupos", "Users");
        }

        public IActionResult VistaTickets()
        {
            List<Ticket> tickets = this.repo.GetTickets();

            return View(tickets);
        }
    }
}
