﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WheatherForecast.Models;
using WheatherForecast.Models.OpenWeatherModels;
using WheatherForecast.Services;

namespace WheatherForecast.Api
{
    public class ForecastController : ApiController
    {
        private readonly IWeatherService _weatherService;

        private readonly IForecastProvider _forecastProvider;

        private readonly IForecastConverter _forecastConverter;

        private const string InvalidCity = "Our service doesn't provide forecast for city '{0}'";

        private const string InvalidDays = "Count of requested days should be in range 1..17";

        public ForecastController(IWeatherService weatherService, IForecastProvider forecastProvider,
            IForecastConverter forecastConverter)
        {
            _weatherService = weatherService;
            _forecastProvider = forecastProvider;
            _forecastConverter = forecastConverter;
        }

        [HttpGet]
        [Route("api/v1/forecast/{city:alpha}/{days:int=1}")]
        public async Task<HttpResponseMessage> ShowForecast(string city, int days)
        {
            if (string.IsNullOrEmpty(city))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            try
            {
                var forecast = await _forecastProvider.GetForecastAsync(city, days);
                var forecastEntity = _forecastConverter.ConvertToEntity(forecast);
                await _weatherService.AddForecastAsync(forecastEntity);
                
                return Request.CreateResponse<ForecastObject>(HttpStatusCode.OK, forecast);
            }
            catch (ArgumentException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, InvalidDays);
            }
            catch (WebException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(InvalidCity, city));
            }
        }
    }
}
