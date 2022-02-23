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
    }
}
