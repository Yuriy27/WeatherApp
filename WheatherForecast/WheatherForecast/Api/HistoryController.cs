using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WheatherForecast.Models;
using WheatherForecast.Services;

namespace WheatherForecast.Api
{
    public class HistoryController : ApiController
    {
        private readonly IWeatherService _weatherService;

        public HistoryController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IEnumerable<ForecastEntity>> GetHistory()
        {
            return (await _weatherService.GetForeacstsAsync()).Reverse();
        }
    }
}
