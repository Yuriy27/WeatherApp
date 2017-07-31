using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WheatherForecast.Models;
using WheatherForecast.Services;

namespace WheatherForecast.Api
{
    public class CitiesController : ApiController
    {
        private const string InvalidCity = "Our service doesn't provide forecast for city '{0}'";

        private readonly IWeatherService _weatherService;

        private readonly IForecastProvider _forecastProvider;

        public CitiesController(IWeatherService weatherService, IForecastProvider forecastProvider)
        {
            _weatherService = weatherService;
            _forecastProvider = forecastProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<CityEntity>> GetDefaultCities()
        {
            return await _weatherService.GetCitiesAsync();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddDefaultCity([FromBody] CityEntity city)
        {
            if (city == null || !ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if ((await _weatherService.GetCitiesByNameAsync(city.Name)).Count() != 0)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, $"City '{city.Name}' already exist");
            }
            if (!await _forecastProvider.SuccessPingCityAsync(city.Name))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(InvalidCity, city.Name));
            }
            await _weatherService.AddCityAsync(city);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteDefaultCity(int id)
        {
            if (await _weatherService.GetCityAsync(id) == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            await _weatherService.DeleteCityAsync(id);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateDefaultCity([FromBody] CityEntity city)
        {
            if (city == null || !ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            if (!await _forecastProvider.SuccessPingCityAsync(city.Name))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(InvalidCity, city.Name));
            }
            var newCity = await _weatherService.GetCityAsync(city.Id);
            if (newCity == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            newCity.Name = city.Name;
            await _weatherService.UpdateCityAsync(newCity);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
