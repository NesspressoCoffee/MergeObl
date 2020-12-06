using BusinessLogic.Helpers;
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
        public ActionResult manVuelo()
        {
            List<DtoAeropuerto> listAeropuerto = HAeropuerto.getInstance().ListarAeros();
            List<SelectListItem> listSelectAeropuerto = new List<SelectListItem>();

            foreach (DtoAeropuerto item in listAeropuerto)
            {
                SelectListItem opcion = new SelectListItem();
                opcion.Text = item.ciudad.ToString() + " (" + item.abreviatura + ")";
                opcion.Value = item.abreviatura.ToString();
                listSelectAeropuerto.Add(opcion);
            }
            ViewBag.listFechasExclusivas = (List<DtoFechaExclusiva>)Session["fechasExclusivas"];
            ViewBag.listAeropuerto = listSelectAeropuerto;
            ViewBag.diasVuelo = (List<string>)Session["diasVuelo"];
            if (Session["dtoDatosVuelo"] != null)
            {
                return View((DtoVuelo)Session["dtoDatosVuelo"]);

            }
            return View();
        }

        public ActionResult manVuelo2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addVuelo(DtoVuelo dto)
        {
            List<string> dias = (List<string>)Session["diasVuelo"];
            List<DtoFechaExclusiva> fechasExclusivas = (List<DtoFechaExclusiva>)Session["fechasExclusivas"];
            HVuelo.getInstance().addVuelo(dto, fechasExclusivas, dias);
            Session["fechasExclusivas"] = null;
            Session["dtoDatosVuelo"] = null;
            Session["diasVuelo"] = null;
            return RedirectToAction("manVuelo");
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

                if (comienzo != DtoDatosVuelo.fechaComienzo || limite != DtoDatosVuelo.fechaLimite)
                {
                    Session["fechasExclusivas"] = null;
                }
            }

            Session["dtoDatosVuelo"] = DtoDatosVuelo;

            return RedirectToAction("manVuelo");
        }
    }
}