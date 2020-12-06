using BusinessLogic.Helpers;
using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace AlasPum_Airlines.Controllers
{
    [Authorize]
    public class AeropuertoController : Controller
    {
       
        // GET: Aeropuerto
        [HttpGet]
        public ActionResult AltaAeropuerto()
        {
            List<DtoAeropuerto> colAeros = HAeropuerto.getInstance().ListarAeros();
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
                HAeropuerto.getInstance().AddAeropuerto(dto, Session["User"].ToString());
                
            }

            return RedirectToAction("AltaAeropuerto");
        }

        public ActionResult DeshabilitarAero(string codigo)
        {

            HAeropuerto.getInstance().DeshabilitarAirport(codigo, Session["User"].ToString());

            return RedirectToAction("AltaAeropuerto");
        }

        public ActionResult HabilitarAero(string codigo)
        {

            HAeropuerto.getInstance().HabilitarAirport(codigo, Session["User"].ToString());

            return RedirectToAction("AltaAeropuerto");
        }

        
        public ActionResult UpdateTasaParcial(string codigo)
        {
          DtoHistoricoTasa dtoPartial = HAeropuerto.getInstance().GetTasaByCode(codigo);
            return PartialView("UpdateTasa", dtoPartial);
        }
        

        [HttpPost]
        public ActionResult ModificarTasa(string codigoAirport, double tasaRegional, double tasaInter)
        {
            HAeropuerto.getInstance().modificarTasa(codigoAirport, tasaRegional, tasaInter, Session["User"].ToString());
            return RedirectToAction("AltaAeropuerto");
        }



        public JsonResult CodigoAvailable(string abreviatura)
        {
            bool ok = HAeropuerto.getInstance().ValidarCodigo(abreviatura);

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