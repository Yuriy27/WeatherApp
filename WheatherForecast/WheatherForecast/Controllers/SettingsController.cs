using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Concrete;
using WheatherForecast.Repositories.Interfaces;
using WheatherForecast.Services;

namespace WheatherForecast.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IForecastProvider _forecastProvider;

        private readonly IWeatherService _weatherService;

        public SettingsController(IForecastProvider forecastProvider, IWeatherService weatherService)
        {
            _forecastProvider = forecastProvider;
            _weatherService = weatherService;
        }

        // GET: Settings
        public ActionResult Index()
        {
            var cities = _weatherService.GetCities();
            return View(cities);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCity(int id)
        {
            _weatherService.DeleteCity(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Add")]
        public ActionResult AddCity(CityEntity city)
        {
            if (string.IsNullOrEmpty(city?.Name))
            {
                return RedirectToAction("Index");
            }
            var view = "~/Views/Settings/Index.cshtml";
            var cities = _weatherService.GetCities();
            if (_weatherService.GetCitiesByName(city.Name).Count() != 0)
            {
                ViewBag.ErrorMessage = $"City '{city.Name}' had already added in default cities.";
                return View(view, cities);
            }
            try
            {
                var forecast = _forecastProvider.GetForecast(city.Name, 1);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"SmartWeather service doesn't provide forecast for city '{city.Name}'";
                return View(view, cities);
            }
            _weatherService.AddCity(city);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdateCity(CityEntity city)
        {
            if (string.IsNullOrEmpty(city?.Name))
            {
                return RedirectToAction("Index");
            }
            _weatherService.UpdateCity(city);

            return RedirectToAction("Index");
        }
    }
}