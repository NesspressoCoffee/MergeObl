using BusinessLogic.Helpers;
using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlasPum_Clientes.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult FormularioCompra(DtoVuelo dto)
        {
            if (Session["DatosVuelo"] == null)
            {
                Session["DatosVuelo"] = dto;
            }
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Débito", Value = "Débito" });
            items.Add(new SelectListItem { Text = "Crédito", Value = "Crédito" });
            ViewBag.tarjeta = items;
            ViewBag.pasajeros = Session["Pasajeros"] as List<DtoPasajero>;

            if (Session["dtoCompra"] != null)
            {
                DtoCompra compra = Session["dtoCompra"] as DtoCompra;
                return View(compra);
            }

            return View();
        }


        public ActionResult AgregarPasajero(string nombre, string apellido, string documento)
        {
            DtoPasajero dto = new DtoPasajero(nombre, apellido, documento);
            List<DtoPasajero> listaPasajeros = new List<DtoPasajero>();
            if (Session["Pasajeros"] != null)
            {
                listaPasajeros = (List<DtoPasajero>)Session["Pasajeros"];
                if (listaPasajeros.Count() != 0) dto.id = listaPasajeros.LastOrDefault().id + 1;
            }
            listaPasajeros.Add(dto);
            Session["Pasajeros"] = listaPasajeros;
            //listaPasajeros = Session["Pasajeros"] as List<DtoPasaje>;
            //listaPasajeros.Add(pasajero);
            return RedirectToAction("FormularioCompra");
        }

        public ActionResult QuitarPasajero(int id)
        {
            List<DtoPasajero> listDto = new List<DtoPasajero>();
            listDto = (List<DtoPasajero>)Session["Pasajeros"];
            DtoPasajero dto = listDto.FirstOrDefault(f => f.id == id);
            listDto.Remove(dto);
            Session["Pasajeros"] = listDto;
            return RedirectToAction("FormularioCompra");
        }

        public ActionResult ConfirmarCompra(DtoCompra dto)
        {
            List<DtoPasajero> pasajeros = Session["Pasajeros"] as List<DtoPasajero>;
            DtoVuelo vuelo = Session["DatosVuelo"] as DtoVuelo;
            DtoCompra datosCompra = Session["dtoCompra"] as DtoCompra;
            int idCompra = HCompra.getInstance().ConfirmarCompra(datosCompra, pasajeros, vuelo);
            datosCompra.idCompra = idCompra;
            Session["dtoCompra"] = datosCompra;


            return RedirectToAction("CompraConfirmada");
        }

        [HttpPost]
        public ActionResult saveDatosInSession(DtoCompra DtoDatosCompra)
        {
            //if (Session["dtoCompra"] != null)
            //{
            //    DtoCompra datos = (DtoCompra)Session["dtoCompra"];
            //}

            Session["dtoCompra"] = DtoDatosCompra;

            return RedirectToAction("FormularioCompra");
        }


        public ActionResult CompraConfirmada()
        {
            DtoCompra datosCompra = Session["dtoCompra"] as DtoCompra;

            return View(datosCompra);
        }

    }
}