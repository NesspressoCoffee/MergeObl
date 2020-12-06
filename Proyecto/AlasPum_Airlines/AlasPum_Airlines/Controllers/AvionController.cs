using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Patrones;
using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlasPum_Airlines.Controllers
{
    //[Authorize]
    public class AvionController : Controller
    {
        private Fachada fachada;

        public AvionController()
        {
            fachada = new Fachada();
        }

        public ActionResult manAvion(string mensaje, string mensajeErrorCategoria, string mensajeErrorDisponibilidad, int? idAvion)
        {
            ViewBag.mensajeErrorCategoria = mensajeErrorCategoria;
            ViewBag.mensajeErrorDisponibilidad = mensajeErrorDisponibilidad;
            ViewBag.mensaje = mensaje;
            ViewBag.avionModify = Session["avionModify"];

            List<DtoAvion> colAvion = fachada.getColAvion();
            ViewBag.colAvion = colAvion;

            //EN CASO DE MODIFICACION
            if (idAvion != null)
            {
                DtoAvion dto = fachada.getAvion(idAvion.Value);
                Session["modify"] = true;
                Session["dtoDatosAvion"] = dto;
                Session["categoriaAsiento"] = fachada.getColCategoriaAsiento(idAvion.Value);
                Session["disponibilidadAsiento"] = fachada.getColDisponibilidadAsiento(idAvion.Value);
                ViewBag.listCategoria = (List<DtoCategoriaAsiento>)Session["categoriaAsiento"];
                ViewBag.listDisponibilidad = (List<DtoDisponibilidadAsiento>)Session["disponibilidadAsiento"];
                ViewBag.modify = true;
                return View(dto);
            }
            //EN CASO DE AGREGAR GRUPO DE CATEGORIA O DISPONIBILIDAD
            else if (Session["dtoDatosAvion"] != null)
            {
                List<DtoCategoriaAsiento> listCategoria = new List<DtoCategoriaAsiento>();
                listCategoria = (List<DtoCategoriaAsiento>)Session["categoriaAsiento"];
                if (listCategoria != null)
                    ViewBag.listCategoria = listCategoria.OrderBy(o => o.desde).ToList();

                List<DtoDisponibilidadAsiento> listDisponibilidad = new List<DtoDisponibilidadAsiento>();
                listDisponibilidad = (List<DtoDisponibilidadAsiento>)Session["disponibilidadAsiento"];
                if (listDisponibilidad != null)
                    ViewBag.listDisponibilidad = listDisponibilidad.OrderBy(o => o.desde).ToList();

                return View((DtoAvion)Session["dtoDatosAvion"]);
            }
            else return View();
        }

        public ActionResult addAvion(DtoAvion nuevoAvion)
        {
            string mensaje = "";
            if (Session["modify"] == null || (bool)Session["modify"] == false)
            {

                List<DtoCategoriaAsiento> listCategoria = new List<DtoCategoriaAsiento>();
                listCategoria = (List<DtoCategoriaAsiento>)Session["categoriaAsiento"];

                List<DtoDisponibilidadAsiento> listDisponibilidad = new List<DtoDisponibilidadAsiento>();
                listDisponibilidad = (List<DtoDisponibilidadAsiento>)Session["disponibilidadAsiento"];

                if (fachada.validarCantidadDeAsientos(nuevoAvion.cantAsientos, listCategoria) && fachada.validarCantidadDeAsientos(nuevoAvion.cantAsientos, listDisponibilidad))
                    fachada.addAvion(nuevoAvion, listCategoria.OrderBy(o => o.desde).ToList(), listDisponibilidad.OrderBy(o => o.desde).ToList());
                else mensaje = "Se debe asignar categoria y diponibilidad para cada asiento";

                if (mensaje == "")
                {
                    Session["dtoDatosAvion"] = null;
                    Session["categoriaAsiento"] = null;
                    Session["disponibilidadAsiento"] = null;

                }

                return RedirectToAction("manAvion", new { mensaje = mensaje });
            }
            else
            {
                List<DtoCategoriaAsiento> listCategoria = new List<DtoCategoriaAsiento>();
                listCategoria = (List<DtoCategoriaAsiento>)Session["categoriaAsiento"];

                List<DtoDisponibilidadAsiento> listDisponibilidad = new List<DtoDisponibilidadAsiento>();
                listDisponibilidad = (List<DtoDisponibilidadAsiento>)Session["disponibilidadAsiento"];

                if (fachada.validarCantidadDeAsientos(nuevoAvion.cantAsientos, listCategoria))
                    fachada.modifyAvion(nuevoAvion, listCategoria.OrderBy(o => o.desde).ToList(), listDisponibilidad.OrderBy(o => o.desde).ToList());
                else mensaje = "Se debe asignar una categoria para cada asiento";

                if (mensaje == "")
                {
                    Session["modify"] = false;
                    Session["avionModify"] = null;
                    Session["dtoDatosAvion"] = null;
                    Session["categoriaAsiento"] = null;
                    Session["disponibilidadAsiento"] = null;

                }

                return RedirectToAction("manAvion", new { mensaje = mensaje });
            }


        }

        public ActionResult modifyAvionListButton(int idAvion)
        {
            Session["avionModify"] = "Aeronave " + idAvion;
            return RedirectToAction("manAvion", new { idAvion = idAvion });
        }

        public ActionResult deleteAvion(int idAvion)
        {
            if (fachada.deleteAvion(idAvion) == 1)
            {
                return RedirectToAction("manAvion", new { mensaje = "La aeronave que intenta eliminar se encuentra asociada a un vuelo pendiente" });
            }
            return RedirectToAction("manAvion");
        }

        [HttpPost]
        public ActionResult saveDatosInSession(DtoAvion DtoDatosAvion)
        {
            if (Session["dtoDatosAvion"] != null)
            {
                DtoAvion datos = (DtoAvion)Session["dtoDatosAvion"];
                int cantAsientos = datos.cantAsientos;

                if (cantAsientos != DtoDatosAvion.cantAsientos)
                {
                    Session["categoriaAsiento"] = null;
                    Session["disponibilidadAsiento"] = null;
                }
            }

            Session["dtoDatosAvion"] = DtoDatosAvion;

            return RedirectToAction("manAvion");
        }

        #region - Categorias Session List -
        [HttpPost]
        public ActionResult addCategoriaToSession(int? desde, int? hasta, string categoria)
        {
            int cantAsientos = 0;
            if (Session["dtoDatosAvion"] != null)
            {
                DtoAvion datos = (DtoAvion)Session["dtoDatosAvion"];
                cantAsientos = datos.cantAsientos;
            }

            DtoCategoriaAsiento dto = new DtoCategoriaAsiento(desde, hasta, categoria);
            List<DtoCategoriaAsiento> listDto = new List<DtoCategoriaAsiento>();
            if (Session["categoriaAsiento"] != null)
            {
                listDto = (List<DtoCategoriaAsiento>)Session["categoriaAsiento"];
                if (listDto.Count() != 0) dto.id = listDto.LastOrDefault().id + 1;
            }
            int ok = fachada.validarGrupoDeAsientos(cantAsientos, listDto, desde, hasta);

            string mensajeErrorCategoria = "";
            if (ok == 0) listDto.Add(dto);
            else if (ok == 1) mensajeErrorCategoria = "El comienzo del rango de asientos debe ser menor al final";
            else if (ok == 2) mensajeErrorCategoria = "La cantidad total de asientos no puede ser superada";
            else if (ok == 3) mensajeErrorCategoria = "El rango contiene asientos con categoria ya asignada";
            else if (ok == 4) mensajeErrorCategoria = "Los limites del rango de asientos no pueden ser negativos";
            else if (ok == 5) mensajeErrorCategoria = "Debe asignar un inicio y un fin al rango de asientos";

            Session["categoriaAsiento"] = listDto;

            return RedirectToAction("manAvion", new { mensajeErrorCategoria = mensajeErrorCategoria });
        }

        public ActionResult removeCategoriaFromSession(int id)
        {
            List<DtoCategoriaAsiento> listDto = new List<DtoCategoriaAsiento>();
            listDto = (List<DtoCategoriaAsiento>)Session["categoriaAsiento"];
            DtoCategoriaAsiento dto = listDto.FirstOrDefault(f => f.id == id);
            listDto.Remove(dto);
            Session["categoriaAsiento"] = listDto;

            return RedirectToAction("manAvion");
        }

        #endregion

        #region - Disponibilidad Session List -
        [HttpPost]
        public ActionResult addDisponibilidadToSession(int? desde, int? hasta, string disponibilidad)
        {
            int cantAsientos = 0;
            if (Session["dtoDatosAvion"] != null)
            {
                DtoAvion datos = (DtoAvion)Session["dtoDatosAvion"];
                cantAsientos = datos.cantAsientos;
            }

            DtoDisponibilidadAsiento dto = new DtoDisponibilidadAsiento(desde, hasta, disponibilidad);
            List<DtoDisponibilidadAsiento> listDto = new List<DtoDisponibilidadAsiento>();
            if (Session["disponibilidadAsiento"] != null)
            {
                listDto = (List<DtoDisponibilidadAsiento>)Session["disponibilidadAsiento"];
                if (listDto.Count() != 0) dto.id = listDto.LastOrDefault().id + 1;
            }
            int ok = fachada.validarGrupoDeAsientos(cantAsientos, listDto, desde, hasta);

            string mensajeErrorDisponibilidad = "";
            if (ok == 0) listDto.Add(dto);
            else if (ok == 1) mensajeErrorDisponibilidad = "El comienzo del rango de asientos debe ser menor al final";
            else if (ok == 2) mensajeErrorDisponibilidad = "La cantidad total de asientos no puede ser superada";
            else if (ok == 3) mensajeErrorDisponibilidad = "El rango contiene asientos con disponibilidad ya asignada";
            else if (ok == 4) mensajeErrorDisponibilidad = "Los limites del rango de asientos no pueden ser negativos";
            else if (ok == 5) mensajeErrorDisponibilidad = "Debe asignar un inicio y un fin al rango de asientos";

            Session["disponibilidadAsiento"] = listDto;

            return RedirectToAction("manAvion", new { mensajeErrorDisponibilidad = mensajeErrorDisponibilidad });
        }

        public ActionResult removeDisponibilidadFromSession(int id)
        {
            List<DtoDisponibilidadAsiento> listDto = new List<DtoDisponibilidadAsiento>();
            listDto = (List<DtoDisponibilidadAsiento>)Session["disponibilidadAsiento"];
            DtoDisponibilidadAsiento dto = listDto.FirstOrDefault(f => f.id == id);
            listDto.Remove(dto);
            Session["disponibilidadAsiento"] = listDto;

            return RedirectToAction("manAvion");
        }

        #endregion

        #region - Validaciones - 
        public JsonResult ValidarExistenciaAvion(int avionId)
        {
            bool ok = fachada.ValidarExistenciaAvion(avionId);
            return Json(ok, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}