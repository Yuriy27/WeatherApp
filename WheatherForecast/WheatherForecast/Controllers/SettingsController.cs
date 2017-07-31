using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public async Task<ActionResult> Index()
        {
            var cities = await _weatherService.GetCitiesAsync();
            return View(cities);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            await _weatherService.DeleteCityAsync(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<ActionResult> AddCity(CityEntity city)
        {
            if (string.IsNullOrEmpty(city?.Name))
            {
                return RedirectToAction("Index");
            }
            var view = "~/Views/Settings/Index.cshtml";
            var cities = await _weatherService.GetCitiesAsync();
            if ((await _weatherService.GetCitiesByNameAsync(city.Name)).Count() != 0)
            {
                ViewBag.ErrorMessage = $"City '{city.Name}' had already added in default cities.";
                return View(view, cities);
            }
            if (!await _forecastProvider.SuccessPingCityAsync(city.Name))
            {
                ViewBag.ErrorMessage = $"SmartWeather service doesn't provide forecast for city '{city.Name}'";
                return View(view, cities);
            } 
            await _weatherService.AddCityAsync(city);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<ActionResult> UpdateCity(CityEntity city)
        {
            if (string.IsNullOrEmpty(city?.Name))
            {
                return RedirectToAction("Index");
            }
            await _weatherService.UpdateCityAsync(city);

            return RedirectToAction("Index");
        }
    }
}