using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WheatherForecast.Models;
using WheatherForecast.Services;

namespace WheatherForecast.Api
{
    public class CitiesController : ApiController
    {
        private const string InvalidCity = "Our service doesn't provide forecast for city '{0}'";

        private IWeatherService _weatherService;

        private IForecastProvider _forecastProvider;

        public CitiesController(IWeatherService weatherService, IForecastProvider forecastProvider)
        {
            _weatherService = weatherService;
            _forecastProvider = forecastProvider;
        }

        [HttpGet]
        public IEnumerable<CityEntity> GetDefaultCities()
        {
            return _weatherService.GetCities();
        }

        [HttpPost]
        public HttpResponseMessage AddDefaultCity([FromBody] CityEntity city)
        {
            if (city == null || !ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (_weatherService.GetCitiesByName(city.Name).Count() != 0)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, $"City '{city.Name}' already exist");
            }
            if (!_forecastProvider.SuccessPingCity(city.Name))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(InvalidCity, city.Name));
            }
            _weatherService.AddCity(city);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteDefaultCity(int id)
        {
            if (_weatherService.GetCity(id) == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            _weatherService.DeleteCity(id);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage UpdateDefaultCity([FromBody] CityEntity city)
        {
            if (city == null || !ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (!_forecastProvider.SuccessPingCity(city.Name))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(InvalidCity, city.Name));
            }
            var newCity = _weatherService.GetCity(city.Id);
            if (newCity == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            newCity.Name = city.Name;
            _weatherService.UpdateCity(newCity);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
