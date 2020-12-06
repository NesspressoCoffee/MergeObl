using BusinessLogic.Helpers;
using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Configuration;
using DataAccess;
using BusinessLogic.Patrones;

namespace AlasPum_Clientes.Controllers
{
    public class BuscarVuelosController : Controller
    {
        private Fachada fachada;

        public BuscarVuelosController()
        {
            fachada = new Fachada();
        }
        // GET: BuscarVuelos
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult GrillaPreciosVuelo(DtoVuelo dto)
        {
            List<DtoVuelo> precioCat = fachada.GetPreciosCategoriasByFecha(dto.numeroVuelo, dto.fechaSalida);

            ViewBag.preciosEconomy = precioCat;
            return View(dto);
        }

        public ActionResult MoverDiasGrilla(string numVuelo, DateTime fechaInicio, DateTime fechaFin)
        {
            DtoVuelo dto = new DtoVuelo();
            dto.numeroVuelo = numVuelo;
            dto.fechaSalida = fechaInicio;
            dto.fechaLlegada = fechaFin;
            return RedirectToAction("GrillaPreciosVuelo", dto);
        }

        public ActionResult DetallarVuelo(int id)
        {
            fachada.GetVueloCompleto(id);
            return View();
        }

        public ActionResult MostrarVuelos(DtoVuelo dto)
        {
            if (dto.fechaLlegada.Year == 1)
            {

                List<DtoVuelo> directos_SoloIda = fachada.VuelosDirectos_SoloIda(dto);
                ViewBag.directFlights_Ida = directos_SoloIda;
                List<DtoVuelo> escala_SoloIda = fachada.VuelosEscala_Ida(dto);
                ViewBag.escaleFlights_Ida = escala_SoloIda;

            }
            else
            {

                List<DtoVuelo> directos_IdaVuelta = fachada.VuelosDirectos_IdaVuelta(dto);
                ViewBag.directFlights_IdaVuelta = directos_IdaVuelta;
                List<DtoVuelo> escala_IdaVuelta = fachada.VuelosEscala_IdaVuelta(dto);
                ViewBag.escaleFlights_IdaVuelta = escala_IdaVuelta;

            }

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1", Value = "1" });
            items.Add(new SelectListItem { Text = "2", Value = "2" });
            items.Add(new SelectListItem { Text = "3", Value = "3" });
            items.Add(new SelectListItem { Text = "4", Value = "4" });
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "6", Value = "6" });
            items.Add(new SelectListItem { Text = "7", Value = "7" });
            items.Add(new SelectListItem { Text = "8", Value = "8" });
            items.Add(new SelectListItem { Text = "9", Value = "9" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });

            ViewBag.asientus = items;

            return View();
        }

        public JsonResult Search(string term)
        {
            List<DtoAeropuerto> lugares = fachada.GetVuelosByLugar(term);

            return Json(lugares);

        }

        public ActionResult GetPreciosByIdVuelo(int idVuelo)
        {
            DtoPrecioCategoria dtoPrecioList = HPrecioCategoria.getInstance().GetPreciosByIdVuelo(idVuelo);
            ViewBag.PreciosCat = dtoPrecioList;
            return View();
        }

    }
}