using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProyectoAgusCMNetCore.Models;
using ProyectoAgusCMNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> LogIn(string username, string password)
        {
            User usu = this.repo.ExisteUser(username,password);

            if (usu != null)
            {
                ClaimsIdentity identity =new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(ClaimTypes.Name, usu.Name);
                identity.AddClaim(claimName);
                Claim claimId =new Claim(ClaimTypes.NameIdentifier, usu.Userid.ToString());
                Claim claimRole =new Claim(ClaimTypes.Role, usu.Groups.ToString());
                Claim claimAdmin = new Claim("Admin", usu.Admin.ToString());
                identity.AddClaim(claimId);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimAdmin);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                return RedirectToAction("Index", "Users");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
            }
            return View();


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

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
