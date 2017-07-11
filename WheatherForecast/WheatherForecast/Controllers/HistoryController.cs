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
    }
}