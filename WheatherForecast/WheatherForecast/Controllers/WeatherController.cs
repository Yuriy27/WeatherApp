using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;
using WheatherForecast.Services;

namespace WheatherForecast.Controllers
{
    public class WeatherController : Controller
    {
        private List<string> _cities;

        private IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _cities = new List<string>() { "Lviv", "Kiev", "Dnipropetrovsk", "Kharkiv", "Odessa" };
        }

        // GET: Weather
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Cities = _cities;
            return View();
        }

        // POST: Weather
        [HttpPost]
        public ActionResult Index(string city, int days = 1)
        {
            ViewBag.Cities = _cities;
            if (string.IsNullOrEmpty(city))
            {
                return View();
            }
            try
            {
                ForecastObject forecast = _weatherService.GetForecast(city, days);
                return View(forecast);
            }
            catch (WebException ex)
            {
                ViewBag.ErrorMessage = $"Weather forecast for city '{city}' not found.";
                return View();
            }         
        }
    }
}