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

        private readonly IUnitOfWork _uow;

        public SettingsController(IForecastProvider forecastProvider, IUnitOfWork uow)
        {
            _forecastProvider = forecastProvider;
            //_uow = new UnitOfWork();
            _uow = uow;
        }

        // GET: Settings
        public ActionResult Index()
        {
            IEnumerable<CityEntity> cities = _uow.Repository<CityEntity>().GetAll();//_context.Cities;
            return View(cities);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCity(int id)
        {
            _uow.Repository<CityEntity>().Delete(id);
            _uow.Save();

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
            var cities = _uow.Repository<CityEntity>().GetAll();//_context.Cities;
            if (cities.Count(c => c.Name.Equals(city.Name)) != 0)
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
            _uow.Repository<CityEntity>().Create(city);
            _uow.Save();

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
            _uow.Repository<CityEntity>().Update(city);
            _uow.Save();

            return RedirectToAction("Index");
        }
    }
}