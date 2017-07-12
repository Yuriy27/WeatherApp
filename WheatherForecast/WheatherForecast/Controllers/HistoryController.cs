using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;

namespace WheatherForecast.Controllers
{
    public class HistoryController : Controller
    {
        private ForecastContext _context;

        public HistoryController()
        {
            _context = new ForecastContext();
        }
        // GET: History
        public ActionResult Index()
        {
            var history = _context.Forecasts.AsEnumerable();
            return View(history.Reverse());
        }

        public ActionResult Search(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return RedirectToAction("Index");
            }
            var view = "~/Views/History/Index.cshtml";
            var history = _context.Forecasts.AsEnumerable()
                .Where(c => c.City == city);
            ViewBag.InfoMessage = $"Results for city '{city}'";
            return View(view, history.Reverse());
        }

        [HttpPost]
        public ActionResult Search(DateTime? date)
        {
            if (!date.HasValue)
            {
                return RedirectToAction("Index");
            }
            var view = "~/Views/History/Index.cshtml";
            var history = _context.Forecasts.AsEnumerable()
                .Where(c => c.Date.ToShortDateString().Equals(date.Value.ToShortDateString()));
            ViewBag.InfoMessage = $"Results for {date.Value.ToShortDateString()}";
            return View(view, history.Reverse());
        }

    }
}