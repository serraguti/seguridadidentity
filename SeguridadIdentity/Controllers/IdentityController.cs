using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeguridadIdentity.Controllers
{
    public class IdentityController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(String username, String password)
        {
            if (username.ToLower() == "admin"
                && password.ToLower() == "admin")
            {
                //CUALQUIER USUARIO VALIDADO DE FORMA EXPLICITA
                //CONTENDRA UNA IDENTIDAD Y CONTENDRA UN PRINCIPAL
                ClaimsIdentity identidad =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme
                        , ClaimTypes.Name, ClaimTypes.Role);
                identidad.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
                identidad.AddClaim(new Claim(ClaimTypes.Name, username));
                ClaimsPrincipal user =
                    new ClaimsPrincipal(identidad);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme
                    , user,
                    new AuthenticationProperties { 
                         IsPersistent = true
                         ,  ExpiresUtc = DateTime.Now.AddMinutes(15)
                    });
                //LO MANDAMOS AL ACTION PROTEGIDO SI SE HA DADO DE ALTA
                return RedirectToAction("Perfil", "Usuarios");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
            }
            return View();
        }
    }
}
