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

namespace AlasPum_Clientes.Controllers
{
    public class BuscarVuelosController : Controller
    {
        // GET: BuscarVuelos
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult GrillaPreciosVuelo(DtoVuelo dto)
        {
            List<DtoVuelo> precioCat = HPrecioCategoria.getInstance().GetPreciosCategoriasByFecha(dto.numeroVuelo, dto.fechaSalida);
           
            ViewBag.preciosEconomy = precioCat;
            return View(dto);
        }

        public ActionResult DireccionarAMuestra(int pepe, int tipo)
        {
            DtoVuelo vueloEncontrado = HVuelo.getInstance().GetVueloCompleto(pepe);
            DtoVuelo fr = vueloEncontrado;
            if (tipo == 1)
            {
                DateTime dateTime = new DateTime(0001, 01, 01, 00, 00, 00);
                fr.fechaLlegada = dateTime;
            }

            return RedirectToAction("MostrarVuelos", vueloEncontrado);
        }


        public ActionResult MoverDiasGrilla(string numVuelo, DateTime fechaInicio, DateTime fechaFin)
        {
            DtoVuelo dto = new DtoVuelo();
            dto.numeroVuelo = numVuelo;
            dto.fechaSalida = fechaInicio;
            dto.fechaLlegada = fechaFin;

            return RedirectToAction("GrillaPreciosVuelo", dto);
        }



        public ActionResult MostrarVuelos(DtoVuelo dto)
        {
            Session["AsientosDeseados"] = dto.cantidadAsientos;

            if (dto.fechaLlegada.Year == 1)
            {

                List<DtoVuelo> directos_SoloIda = HVuelo.getInstance().VuelosDirectos_SoloIda(dto);
                ViewBag.directFlights_Ida = directos_SoloIda;
                List<DtoVuelo> escala_SoloIda = HVuelo.getInstance().VuelosEscala_Ida(dto);
                ViewBag.escaleFlights_Ida = escala_SoloIda;

            }
            else
            {

                List<DtoVuelo> directos_IdaVuelta = HVuelo.getInstance().VuelosDirectos_IdaVuelta(dto);
                ViewBag.directFlights_IdaVuelta = directos_IdaVuelta;
                List<DtoVuelo> escala_IdaVuelta = HVuelo.getInstance().VuelosEscala_IdaVuelta(dto);
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


            List<DtoAeropuerto> lugares = HVuelo.getInstance().GetVuelosByLugar(term);

            return Json(lugares);

        }

        public ActionResult GetPreciosByIdVuelo(int idVuelo)
        {
            DtoPrecioCategoria dtoPrecioList = HPrecioCategoria.getInstance().GetPreciosByIdVuelo(idVuelo);
            ViewBag.PreciosCat = dtoPrecioList;
            return View();
        }

        #region
        public ActionResult DetallarVuelo(int idVuelo, int avionId)
        {
            DtoVuelo vueloEncontrado = HVuelo.getInstance().GetVueloCompleto(idVuelo);

            List<DtoAsiento> colAsientosDisponibles = HVuelo.getInstance().GetAsientosDisponibles(idVuelo, avionId);
            List<DtoAsientosDispByCategoria> asientosByCategoria = new List<DtoAsientosDispByCategoria>();

            foreach (DtoAsiento item in colAsientosDisponibles)
            {
                if (asientosByCategoria.Count != 0)
                {
                    foreach (DtoAsientosDispByCategoria aux in asientosByCategoria)
                    {
                        if (aux.categoria != item.categoria)
                        {
                            DtoAsientosDispByCategoria asientos = new DtoAsientosDispByCategoria();
                            asientos.categoria = item.categoria;
                            asientosByCategoria.Add(asientos);
                        }

                    }

                }
                else
                {
                    DtoAsientosDispByCategoria asientos = new DtoAsientosDispByCategoria();
                    asientos.categoria = item.categoria;
                    asientosByCategoria.Add(asientos);
                }

            }

            List<SelectListItem> pepe = new List<SelectListItem>();
            foreach (DtoAsientosDispByCategoria aux in asientosByCategoria)
            {
                SelectListItem item = new SelectListItem();
                item.Text = aux.categoria;
                item.Value = aux.categoria;
                pepe.Add(item);

            }
            ViewBag.AsientosDispByCategoria = pepe;
            return View(vueloEncontrado);
        }

        #endregion
    }
}