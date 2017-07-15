using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheatherForecast.Models;
using WheatherForecast.Models.OpenWeatherModels;

namespace WheatherForecast.Services
{
    public interface IForecastConverter
    {
        ForecastEntity ConvertToEntity(ForecastObject obj);
    }
}