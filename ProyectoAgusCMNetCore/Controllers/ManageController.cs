using Microsoft.AspNetCore.Mvc;
using ProyectoAgusCMNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    public class ManageController : Controller
    {
        private RepositoryApp repo;

        public ManageController(RepositoryApp repo)
        {
            this.repo = repo;
        }


        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string correo, string password)
        {
            return RedirectToAction("Index", "Users");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string nombre, string apellidos,string username, string usermail, string userpass)
        {
            this.repo.CreateUser(nombre, apellidos, username, usermail, userpass);
            return RedirectToAction("LogIn");
        }
    }
}
