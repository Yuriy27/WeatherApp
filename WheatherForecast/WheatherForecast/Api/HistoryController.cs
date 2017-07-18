using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WheatherForecast.Models;
using WheatherForecast.Services;

namespace WheatherForecast.Api
{
    public class HistoryController : ApiController
    {
        private IWeatherService _weatherService;

        public HistoryController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IEnumerable<ForecastEntity> GetHistory()
        {
            return _weatherService.GetForeacsts();
        }
    }
}
