using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Concrete;
using WheatherForecast.Repositories.Interfaces;
using WheatherForecast.Services;

namespace WheatherForecast.Controllers
{
    public class WeatherController : Controller
    {
        private List<string> _cities;

        private IForecastProvider _forecastProvider;

        private IWeatherService _weatherService;

        public WeatherController(IForecastProvider forecastProvider, IWeatherService weatherService)
        {
            _forecastProvider = forecastProvider;
            _weatherService = weatherService;
            _cities = _weatherService.GetCityNames().ToList();
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
                ForecastObject forecast = _forecastProvider.GetForecast(city, days);
                ForecastEntity entity = (ForecastEntity)forecast;
                _weatherService.AddForecast(entity);
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