using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prueba.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addAvion(DtoVuelo vuelo)
        {
            return RedirectToAction("Index");
        }
    }
}