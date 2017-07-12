using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Concrete;
using WheatherForecast.Services;

namespace WheatherForecast.Controllers
{
    public class WeatherController : Controller
    {
        private List<string> _cities;

        private IWeatherService _weatherService;

        private UnitOfWork _unitOfWork;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _unitOfWork = new UnitOfWork();
            _cities = _unitOfWork.Cities.GetAll().Select(c => c.Name).ToList();
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
                ForecastEntity entity = (ForecastEntity)forecast;
                _unitOfWork.Forecasts.Create(entity);
                _unitOfWork.Save();
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