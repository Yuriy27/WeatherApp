using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Concrete;
using WheatherForecast.Repositories.Interfaces;
using WheatherForecast.Services;

namespace WheatherForecast.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IWeatherService _weatherService;

        public HistoryController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        // GET: History
        public ActionResult Index()
        {
            var history = _weatherService.GetForeacsts();
            return View(history.Reverse());
        }

        public ActionResult Search(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return RedirectToAction("Index");
            }
            var view = "~/Views/History/Index.cshtml";
            var history = _weatherService.GetForecastsByCity(city);
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
            var dateTime = date.Value; 
            var view = "~/Views/History/Index.cshtml";
            var history = _weatherService.GetForecastsByDate(dateTime);
            ViewBag.InfoMessage = $"Results for {dateTime.ToShortDateString()}";
            return View(view, history.Reverse());
        }

    }
}