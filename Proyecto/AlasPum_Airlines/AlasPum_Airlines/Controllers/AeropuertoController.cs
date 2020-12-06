using BusinessLogic.Helpers;
using BusinessLogic.Patrones;
using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace AlasPum_Airlines.Controllers
{

    public class AeropuertoController : Controller
    {
        private Fachada fachada;

        public AeropuertoController()
        {
            fachada = new Fachada();
        }
        // GET: Aeropuerto
        [HttpGet]
        public ActionResult AltaAeropuerto()
        {
            List<DtoAeropuerto> colAeros = fachada.ListarAeros();
            ViewBag.colAeros = colAeros;

            return View();
        }

        public ActionResult AgregarAeropuerto(DtoAeropuerto dto)
        {
            if (dto.historico.tasaRegional == 0 || dto.historico.tasaInter == 0)
            {
                MessageBox.Show("Existen Tasas sin ingresar!", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                ModelState.AddModelError("Existe", "Existen Tasas sin ingresar!");
            }
            else
            {
                fachada.AddAeropuerto(dto, Session["User"].ToString());

            }

            return RedirectToAction("AltaAeropuerto");
        }

        public ActionResult DeshabilitarAero(string codigo)
        {

            fachada.DeshabilitarAirport(codigo, Session["User"].ToString());

            return RedirectToAction("AltaAeropuerto");
        }

        public ActionResult HabilitarAero(string codigo)
        {

            fachada.HabilitarAirport(codigo, Session["User"].ToString());

            return RedirectToAction("AltaAeropuerto");
        }


        public ActionResult UpdateTasaParcial(string codigo)
        {
            DtoHistoricoTasa dtoPartial = fachada.GetTasaByCode(codigo);
            return PartialView("UpdateTasa", dtoPartial);
        }


        [HttpPost]
        public ActionResult ModificarTasa(string codigoAirport, double tasaRegional, double tasaInter)
        {
            fachada.modificarTasa(codigoAirport, tasaRegional, tasaInter, Session["User"].ToString());
            return RedirectToAction("AltaAeropuerto");
        }



        public JsonResult CodigoAvailable(string abreviatura)
        {
            bool ok = fachada.ValidarCodigo(abreviatura);

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

        #region - VALIDACIONES - 

        public JsonResult ValidarDestino(string destino, string origen)
        {
            bool ok = true;
            if (origen == destino)
            {
                ok = false;
            }
            return Json(ok, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}