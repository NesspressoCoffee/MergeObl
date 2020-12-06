using BusinessLogic.Patrones;
using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlasPum_Airlines.Controllers
{
    public class ReporteController : Controller
    {
        private Fachada fachada;

        public ReporteController()
        {
            fachada = new Fachada();
        }

        public ActionResult viewReporte(string error)
        {
            ViewBag.Error = error;

            if (Session["MasAsientosVacios"] != null)
                ViewBag.MasAsientosVacios = (DtoVuelo)Session["MasAsientosVacios"];

            if (Session["MenosAsientosVacios"] != null)
                ViewBag.MenosAsientosVacios = (DtoVuelo)Session["MenosAsientosVacios"];

            if (Session["Cliente"] != null)
                ViewBag.Cliente = (DtoCliente)Session["Cliente"];

            if (Session["PorcentajeOcupacion"] != null)
                ViewBag.PorcentajeOcupacion = (float)Session["PorcentajeOcupacion"];


            return View();
        }

        [HttpPost]
        public ActionResult doReporte(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            if (fechaDesde == null || fechaHasta == null)
            {
                return RedirectToAction("viewReporte", new { error = "Debe ingresar un rango de fechas valido" });

            }
            else if (fechaHasta < fechaDesde)
            {
                return RedirectToAction("viewReporte", new { error = "El comienzo del rango no puede ser mayor al fin" });
            }

            DateTime desde = fechaDesde.Value;
            DateTime hasta = fechaHasta.Value;
            float porcentajeOcupacion = fachada.porcentajeDeOcupacionByFechas(desde, hasta);
            DtoCliente clienteConMasCompras = fachada.clienteConMasComprasByFechas(desde, hasta);
            List<DtoReporteAsientosVacios> vuelosMasMenosAsientoVacios = fachada.masMenosAsientosOcupadosByFecha(desde, hasta);
            if (vuelosMasMenosAsientoVacios[0].asientosVacios > vuelosMasMenosAsientoVacios[1].asientosVacios)
            {
                Session["MasAsientosVacios"] = vuelosMasMenosAsientoVacios[0].vuelo;
                Session["MenosAsientosVacios"] = vuelosMasMenosAsientoVacios[1].vuelo;
            }
            else
            {
                Session["MasAsientosVacios"] = vuelosMasMenosAsientoVacios[1].vuelo;
                Session["MenosAsientosVacios"] = vuelosMasMenosAsientoVacios[0].vuelo;
            }

            Session["Cliente"] = clienteConMasCompras;

            if (porcentajeOcupacion.ToString() != "NaN")
            {
                Session["PorcentajeOcupacion"] = porcentajeOcupacion;
            }
            else
            {
                Session["PorcentajeOcupacion"] = null;
            }
            return RedirectToAction("viewReporte");


        }
    }
}