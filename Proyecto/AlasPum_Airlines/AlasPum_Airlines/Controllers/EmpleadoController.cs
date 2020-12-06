using BusinessLogic.Helpers;
using BusinessLogic.Patrones;
using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlasPum_Airlines.Controllers
{
    [Authorize]
    public class EmpleadoController : Controller
    {
        private Fachada fachada;

        public EmpleadoController()
        {
            fachada = new Fachada();
        }
        // GET: Empleado
        public ActionResult AdminEmpleados()
        {
            List<DtoEmpleado> colUsuarios = fachada.ListarEmpleados();
            ViewBag.colUsuarios = colUsuarios;
            return View();
        }

        public ActionResult AddEmpleado(DtoEmpleado dto)
        {

            fachada.AddEmpleado(dto, Session["User"].ToString());
            ModelState.Clear();
            return RedirectToAction("AdminEmpleados");
        }

        public ActionResult GetEmpleadoByData(string data)
        {
            List<DtoEmpleado> colUsuarios = fachada.GetEmpleadosByData(data);
            ViewBag.colUsuarios = colUsuarios;
            return View();
        }

        public ActionResult BajaEmpleado(string usuario)
        {
            fachada.BajaEmpleado(usuario);

            if (Session["User"].ToString() == usuario || Session["User"].ToString() == null)
            {
                return RedirectToAction("LogOut", "Login");
            }

            return RedirectToAction("AdminEmpleados");
        }

        public JsonResult UserAvailable(string documentoEmpleado)
        {
            bool ok = fachada.ValidarUser(documentoEmpleado);

            if (ok == true)
            {
                ok = false;
            }
            else
            {
                ok = true;
            }

            return Json(ok, JsonRequestBehavior.AllowGet);
        }


    }
}