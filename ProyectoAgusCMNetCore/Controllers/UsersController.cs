using Microsoft.AspNetCore.Mvc;
using ProyectoAgusCMNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    [AuthorizeUsers]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NuevaPeticion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NuevaPeticion(string titulo)
        {
            return RedirectToAction("Index");
        }
    }
}
