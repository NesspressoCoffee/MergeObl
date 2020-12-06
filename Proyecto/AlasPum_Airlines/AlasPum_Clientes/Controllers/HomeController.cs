using CommonSolutions.DTO;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlasPum_Clientes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          
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


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Carrousel()
        {
            return View();
        }

        public ActionResult GetSearchValue(string search)
        {
            alasdbEntities db = new alasdbEntities();
          
            var result = (from u in db.Aeropuerto.ToList() where u.pais.ToLower().Contains(search.ToLower()) || u.ciudad.ToLower().Contains(search.ToLower()) select new { value = u.ciudad, u.pais }
            );

            return Json(result);
        }
    }
}