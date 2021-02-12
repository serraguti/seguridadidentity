using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeguridadIdentity.Filters
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute
        , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //AQUI EL CODIGO DE LA VALIDACION
            //CUANDO UTILIZAMOS SEGURIDAD TENEMOS AL USUARIO EN
            //context.HttpContext.User  IsAuthenticated
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                //EL USUARIO NO EXISTE, AL LOGIN
                //PARA PODER CAMBIAR EL SENTIDO DE LA NAVEGACION
                //INTERCEPTANDO UNA LLAMADA, DEBEMOS HACERLO
                //CON RUTAS
                RouteValueDictionary rutalogin =
                    new RouteValueDictionary(new
                    {
                         controller = "Identity",
                         action = "Login"
                    });
                RedirectToRouteResult result =
                    new RedirectToRouteResult(rutalogin);
                context.Result = result;
            }
        }
    }
}
