using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheatherForecast.Models;
using Newtonsoft.Json;
using WheatherForecast.Models.OpenWeatherModels;

namespace WheatherForecast.Services
{
    public interface IForecastProvider
    {
        ForecastObject GetForecast(string city, int days);

        bool SuccessPingCity(string city);
    }
}
