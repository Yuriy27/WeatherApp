using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var view = "~/Views/Settings/Index.cshtml";
            var cities = _context.Cities;
            if (cities.Count(c => c.Name.Equals(city.Name)) != 0)
            {
                ViewBag.ErrorMessage = $"City '{city.Name}' had already added in default cities.";
                return View(view, cities);
            }
            try
            {
                var forecast = _weatherService.GetForecast(city.Name, 1);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"SmartWeather service doesn't provide forecast for city '{city.Name}'";
                return View(view, cities);
            }
            cities.Add(city);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Update")]
        public ActionResult UpdateCity(CityEntity city)
        {
            if (city == null || string.IsNullOrEmpty(city.Name))
            {
                return RedirectToAction("Index");
            }
            _context.Entry(city).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}