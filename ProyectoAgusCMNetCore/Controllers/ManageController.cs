using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NugetProyectoAgus;
using ProyectoAgusCMNetCore.Models;
using ProyectoAgusCMNetCore.Repositories;
using ProyectoAgusCMNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoAgusCMNetCore.Controllers
{
    public class ManageController : Controller
    {
        private ServiceApiProyecto service;

        public ManageController(ServiceApiProyecto service)
        {
            this.service = service;
        }


        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string username, string password)
        {
            string token = await this.service.GetTokenAsync(username, password);
            if (token == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
            else
            {
                //UNA VEZ QUE TENEMOS EL TOKEN, RECUPERAMOS EL PERFIL DEL EMPLEADO
                //Y ALMACENAMOS LOS DATOS DEL USUARIO DE FORMA PERSONALIZADA
                User user = await this.service.PerfilUser(token);
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.LastName));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Userid.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Groups));
                identity.AddClaim(new Claim("Admin", user.Admin.ToString()));
                identity.AddClaim(new Claim("TOKEN", token));
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    });
                return RedirectToAction("Index", "Users");
            }
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string apellidos, string username, string usermail, string userpass)
        {
            await this.service.CreateUser(nombre, apellidos, username, usermail, userpass);
            return RedirectToAction("LogIn");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
