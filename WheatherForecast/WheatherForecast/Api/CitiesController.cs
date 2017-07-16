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
        private IWeatherService _weatherService;

        public CitiesController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IEnumerable<string> GetDefaultCities()
        {
            return _weatherService.GetCityNames();
        }

        [HttpPost]
        public HttpResponseMessage AddDefaultCity([FromBody] CityEntity city)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (_weatherService.GetCitiesByName(city.Name).Count() != 0)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            _weatherService.AddCity(city);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteDefaultCity(int id)
        {
            //if (_weatherService.)
            _weatherService.DeleteCity(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
