using BusinessLogic.Helpers;
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
        // GET: Empleado
        public ActionResult AdminEmpleados()
        {
            List<DtoEmpleado> colUsuarios = HEmpleado.getInstance().ListarEmpleados();
            ViewBag.colUsuarios = colUsuarios;
            return View();
        }

        public ActionResult AddEmpleado(DtoEmpleado dto)
        {

            HEmpleado.getInstance().AddEmpleado(dto, Session["User"].ToString());
            ModelState.Clear();
            return RedirectToAction("AdminEmpleados");
        }

        public ActionResult GetEmpleadoByData(string data)
        {
            List<DtoEmpleado> colUsuarios = HEmpleado.getInstance().GetEmpleadosByData(data);
            ViewBag.colUsuarios = colUsuarios;
            return View();
        }

        public ActionResult BajaEmpleado(string usuario)
        {
            HEmpleado.getInstance().BajaEmpleado(usuario);

            if (Session["User"].ToString() == usuario || Session["User"].ToString() == null)
            {
                return RedirectToAction("LogOut", "Login");
            }

            return RedirectToAction("AdminEmpleados");
        }

        public JsonResult UserAvailable(string documentoEmpleado)
        {
            bool ok = HEmpleado.getInstance().ValidarUser(documentoEmpleado);

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