using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheatherForecast.Models;
using Newtonsoft.Json;

namespace WheatherForecast.Services
{
    interface IWeatherService
    {
        ForecastObject GetForecast(string city, int days);
    }
}
