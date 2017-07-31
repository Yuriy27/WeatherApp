using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WheatherForecast.Models;
using WheatherForecast.Models.OpenWeatherModels;
using WheatherForecast.Repositories.Concrete;
using WheatherForecast.Repositories.Interfaces;
using WheatherForecast.Services;

namespace WheatherForecast.Controllers
{
    public class WeatherController : Controller
    {
        private List<string> _cities;

        private readonly IForecastProvider _forecastProvider;

        private readonly IWeatherService _weatherService;

        private readonly IForecastConverter _converter;

        public WeatherController(IForecastProvider forecastProvider, IWeatherService weatherService, IForecastConverter converter)
        {
            _forecastProvider = forecastProvider;
            _weatherService = weatherService;
            _converter = converter;
            InitCities();
        }

        private void InitCities()
        {
            _cities = _weatherService.GetCityNamesAsync().Result.ToList();
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
        public async Task<ActionResult> Index(string city, int days = 1)
        {
            ViewBag.Cities = _cities;
            if (string.IsNullOrEmpty(city))
            {
                return View();
            }
            try
            {
                ForecastObject forecast = await _forecastProvider.GetForecastAsync(city, days);
                ForecastEntity entity = _converter.ConvertToEntity(forecast);
                await _weatherService.AddForecastAsync(entity);
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