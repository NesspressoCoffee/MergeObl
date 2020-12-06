using BusinessLogic.Helpers;
using BusinessLogic.Patrones;
using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AlasPum_Airlines.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private Fachada fachada;

        public LoginController()
        {
            fachada = new Fachada();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                RedirectToAction("Admin", "Home");
            }

            return View();
        }

        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            return Redirect("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ValidarLogin(DtoLogin dto)
        {

            if (ModelState.IsValid && fachada.LoginEmpleado(dto))
            {
                Session["User"] = dto.documentoEmpleado;
                Session["Usuario"] = fachada.GetEmpleadoByDoc(dto.documentoEmpleado).nombreEmpleado;

                FormsAuthentication.SetAuthCookie(dto.documentoEmpleado, false);

                return RedirectToAction("Admin", "Home");

            }
            else if (ModelState.IsValid && fachada.GetEmpleadoByDoc(dto.documentoEmpleado).enServicio == false)
            {
                ModelState.AddModelError("Contraseña", "Usuario deshabilitado!");
            }
            else
            {
                ModelState.AddModelError("Contraseña", "Su usuario o contraseña son incorrectos!");
            }

            return View("Login");
        }


    }
}