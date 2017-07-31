using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            var history = await _weatherService.GetForeacstsAsync();
            return View(history.Reverse());
        }

        public async Task<ActionResult> Search(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return RedirectToAction("Index");
            }
            var view = "~/Views/History/Index.cshtml";
            var history = await _weatherService.GetForecastsByCityAsync(city);
            ViewBag.InfoMessage = $"Results for city '{city}'";
            return View(view, history.Reverse());
        }

        [HttpPost]
        public async Task<ActionResult> Search(DateTime? date)
        {
            if (!date.HasValue)
            {
                return RedirectToAction("Index");
            }
            var dateTime = date.Value; 
            var view = "~/Views/History/Index.cshtml";
            var history = await _weatherService.GetForecastsByDateAsync(dateTime);
            ViewBag.InfoMessage = $"Results for {dateTime.ToShortDateString()}";
            return View(view, history.Reverse());
        }

    }
}