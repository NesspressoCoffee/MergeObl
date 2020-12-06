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
    public class VueloController : Controller
    {
        private Fachada fachada;

        public VueloController()
        {
            fachada = new Fachada();
        }

        public ActionResult manVuelo(int? idVuelo, string mensaje)
        {
            List<DtoAeropuerto> listAeropuerto = fachada.ListarAeros();
            List<SelectListItem> listSelectAeropuerto = new List<SelectListItem>();

            foreach (DtoAeropuerto item in listAeropuerto)
            {
                SelectListItem opcion = new SelectListItem();
                opcion.Text = item.ciudad.ToString() + " (" + item.abreviatura + ")";
                opcion.Value = item.abreviatura.ToString();
                listSelectAeropuerto.Add(opcion);
            }
            ViewBag.mensaje = mensaje;
            ViewBag.listEstado = fachada.getListEstado();
            ViewBag.listVisa = fachada.getListVisa();
            ViewBag.colVuelo = fachada.getColVuelo().OrderBy(g => g.estado);
            ViewBag.listFechasExclusivas = (List<DtoFechaExclusiva>)Session["fechasExclusivas"];
            ViewBag.listAeropuerto = listSelectAeropuerto;
            ViewBag.diasVuelo = (List<string>)Session["diasVuelo"];

            if (idVuelo != null)
            {
                DtoVuelo dto = fachada.getVuelo(idVuelo.Value);
                Session["dtoDatosVuelo"] = dto;
                Session["modify"] = true;
                ViewBag.modify = true;
                return View(dto);
            }
            if (Session["dtoDatosVuelo"] != null)
            {
                return View((DtoVuelo)Session["dtoDatosVuelo"]);

            }
            else return View();
        }

        public ActionResult addVuelo(DtoVuelo dto)
        {
            if (Session["modify"] == null || (bool)Session["modify"] == false)
            {
                List<string> dias = (List<string>)Session["diasVuelo"];
                if (dias == null || dias.Count() == 0) return RedirectToAction("manVuelo", new { mensaje = "Debe seleccionar los dias de vuelo" });

                List<DtoFechaExclusiva> fechasExclusivas = (List<DtoFechaExclusiva>)Session["fechasExclusivas"];
                if (fachada.addVuelo(dto, fechasExclusivas, dias) == 1)
                {
                    return RedirectToAction("manVuelo", new { mensaje = "Avion no disponible para las fechas seleccionadas" });
                }
                Session["fechasExclusivas"] = null;
                Session["dtoDatosVuelo"] = null;
                Session["diasVuelo"] = null;
                Session["modify"] = false;
                return RedirectToAction("manVuelo");
            }
            else
            {

                if(fachada.modifyVuelo((DtoVuelo)Session["dtoDatosVuelo"]) == 1)
                {
                    DtoVuelo vuelo = (DtoVuelo)Session["dtoDatosVuelo"];
                    return RedirectToAction("manVuelo", new { mensaje = "Avion no disponible para las fechas seleccionadas", idVuelo = vuelo.idVuelo });
                }
                Session["fechasExclusivas"] = null;
                Session["dtoDatosVuelo"] = null;
                Session["diasVuelo"] = null;
                Session["modify"] = false;
                return RedirectToAction("manVuelo");
            }
        }

        public ActionResult deleteVuelo(int idVuelo)
        {
            fachada.deleteVuelo(idVuelo);
            return RedirectToAction("manVuelo");
        }

        public ActionResult modifyVuelo(int idVuelo)
        {
            return RedirectToAction("manVuelo", new { idVuelo = idVuelo });
        }

        #region - Fecha Session -
        [HttpPost]
        public ActionResult addFechaToSession(DtoFechaExclusiva dto)
        {
            string mensaje = "";
            List<DtoFechaExclusiva> fechas = new List<DtoFechaExclusiva>();
            if (Session["fechasExclusivas"] != null)
            {
                fechas = (List<DtoFechaExclusiva>)Session["fechasExclusivas"];

            }
            if (fechas.Any(a => a.fecha == dto.fecha))
            {

                mensaje = "Esa fecha ya se ingreso en la lista";

            }
            else if (dto.fecha == null)
            {
                mensaje = "Debe ingresar una fecha valida";

            }
            else fechas.Add(dto);

            Session["fechasExclusivas"] = fechas;

            return RedirectToAction("manVuelo");
        }

        public ActionResult removeFechaFromSession(DateTime? fecha)
        {
            List<DtoFechaExclusiva> fechas = new List<DtoFechaExclusiva>();
            fechas = (List<DtoFechaExclusiva>)Session["fechasExclusivas"];
            DtoFechaExclusiva dto = fechas.FirstOrDefault(f => f.fecha == fecha);
            fechas.Remove(dto);
            Session["fechasExclusivas"] = fechas;

            return RedirectToAction("manVuelo");
        }
        #endregion

        [HttpPost]
        public ActionResult saveDiaInSession(string dataDia)
        {
            List<string> dias = new List<string>();
            if (Session["diasVuelo"] != null)
            {
                dias = (List<string>)Session["diasVuelo"];

            }

            if (dias.Any(a => a == dataDia))
            {
                dias.Remove(dataDia);
            }
            else
            {
                dias.Add(dataDia);
            }
            Session["diasVuelo"] = dias;

            return RedirectToAction("manVuelo");
        }

        [HttpPost]
        public ActionResult saveDatosInSession(DtoVuelo DtoDatosVuelo)
        {
            if (Session["dtoDatosVuelo"] != null)
            {
                DtoVuelo datos = (DtoVuelo)Session["dtoDatosVuelo"];
                DateTime comienzo = datos.fechaComienzo;
                DateTime limite = datos.fechaLimite;
                DateTime salida = datos.fechaSalida;

                if (comienzo != DtoDatosVuelo.fechaComienzo || limite != DtoDatosVuelo.fechaLimite)
                {
                    Session["fechasExclusivas"] = null;
                }
                if (salida != DtoDatosVuelo.fechaSalida)
                {
                    DtoDatosVuelo.fechaLlegada = DtoDatosVuelo.fechaSalida.AddHours(DtoDatosVuelo.horaSalida.Hour);
                }
            }

            Session["dtoDatosVuelo"] = DtoDatosVuelo;

            return RedirectToAction("manVuelo");
        }

        #region - Validaciones - 
        //public JsonResult validarAeropuertos(string destino)
        //{
        //    if (destino == "hola")
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);

        //    }
        //    else return Json(true, JsonRequestBehavior.AllowGet);

        //}
        public JsonResult ValidarFechaLimite(DateTime fechaComienzo, DateTime fechaLimite)
        {
            bool ok = true;
            if (fechaLimite < fechaComienzo)
            {
                ok = false;

            }
            return Json(ok, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ValidarFechaComienzo(DateTime fechaComienzo)
        {
            bool ok = true;
            if (DateTime.Now >= fechaComienzo)
            {
                ok = false;

            }
            return Json(ok, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ValidarPrecioEconomy(float precioEconomy)
        {
            bool ok = true;
            if (Session["dtoDatosVuelo"] != null)
            {
                DtoVuelo dto = (DtoVuelo)Session["dtoDatosVuelo"];
                List<DtoCategoriaAsiento> asientos = fachada.getColCategoriaAsiento(dto.avionId);
                if (precioEconomy == 0 && asientos.Any(a => a.categoria == "Economy"))
                {
                    ok = false;
                }
            }
            return Json(ok, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarPrecioBusiness(float precioBusiness)
        {
            bool ok = true;
            if (Session["dtoDatosVuelo"] != null)
            {
                DtoVuelo dto = (DtoVuelo)Session["dtoDatosVuelo"];
                List<DtoCategoriaAsiento> asientos = fachada.getColCategoriaAsiento(dto.avionId);
                if (precioBusiness == 0 && asientos.Any(a => a.categoria == "Business"))
                {
                    ok = false;
                }
            }
            return Json(ok, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarPrecioPremium(float precioPremium)
        {
            bool ok = true;
            if (Session["dtoDatosVuelo"] != null)
            {
                DtoVuelo dto = (DtoVuelo)Session["dtoDatosVuelo"];
                List<DtoCategoriaAsiento> asientos = fachada.getColCategoriaAsiento(dto.avionId);
                if (precioPremium == 0 && asientos.Any(a => a.categoria == "Premium"))
                {
                    ok = false;
                }
            }
            return Json(ok, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarPrecioFirstClass(float precioFirstClass)
        {
            bool ok = true;
            if (Session["dtoDatosVuelo"] != null)
            {
                DtoVuelo dto = (DtoVuelo)Session["dtoDatosVuelo"];
                List<DtoCategoriaAsiento> asientos = fachada.getColCategoriaAsiento(dto.avionId);
                if (precioFirstClass == 0 && asientos.Any(a => a.categoria == "First Class"))
                {
                    ok = false;
                }
            }
            return Json(ok, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarNumeroVuelo(string numeroVuelo)
        {
            if (Session["modify"] == null || (bool)Session["modify"] == false)
            {
                bool ok = fachada.validarNumeroVuelo(numeroVuelo);
                return Json(ok, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}