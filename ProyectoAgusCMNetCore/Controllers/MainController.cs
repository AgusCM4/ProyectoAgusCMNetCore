using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    public class MainController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
    }
}
