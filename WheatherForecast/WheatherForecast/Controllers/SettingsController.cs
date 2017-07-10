using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;
using WheatherForecast.Services;

namespace WheatherForecast.Controllers
{
    public class SettingsController : Controller
    {
        private ForecastContext _context;

        private IWeatherService _weatherService;

        public SettingsController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _context = new ForecastContext();
        }

        // GET: Settings
        public ActionResult Index()
        {
            IEnumerable<CityEntity> cities = _context.Cities;
            return View(cities);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCity(int id)
        {
            var city = _context.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            _context.Cities.Remove(city);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Add")]
        public ActionResult AddCity(CityEntity city)
        {
            if (city == null || string.IsNullOrEmpty(city.Name))
            {
                return RedirectToAction("Index");
            }
            var cities = _context.Cities;
            if (cities.Contains(city))
            {
                ViewBag.ErrorMessage = $"City '{city.Name}' had already added in default cities.";
            }
            try
            {
                var forecast = _weatherService.GetForecast(city.Name, 1);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"SmartWeather service doesn't provide forecast for city '{city.Name}'";
            }
            cities.Add(city);
            _context.SaveChanges();

            return RedirectToAction("Index");//View("~/Views/Settings/Index.cshtml");
        }
    }
}