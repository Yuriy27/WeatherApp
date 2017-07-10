using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WheatherForecast.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("Delete")]
        public ActionResult DeleteCity(string city)
        {
            return null;
        }

        [ActionName("Add")]
        public RedirectToRouteResult AddCity(string city)
        {
            return RedirectToAction("Index");
        }
    }
}