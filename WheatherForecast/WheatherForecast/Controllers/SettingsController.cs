using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Concrete;
using WheatherForecast.Services;

namespace WheatherForecast.Controllers
{
    public class SettingsController : Controller
    {

        private IWeatherService _weatherService;

        private UnitOfWork _unitOfWork;

        public SettingsController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _unitOfWork = new UnitOfWork();
        }

        // GET: Settings
        public ActionResult Index()
        {
            IEnumerable<CityEntity> cities = _unitOfWork.Cities.GetAll();//_context.Cities;
            return View(cities);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCity(int id)
        {
            _unitOfWork.Cities.Delete(id);
            _unitOfWork.Save();

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
            var cities = _unitOfWork.Cities.GetAll();//_context.Cities;
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
            _unitOfWork.Cities.Create(city);
            _unitOfWork.Save();

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
            _unitOfWork.Cities.Update(city);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}