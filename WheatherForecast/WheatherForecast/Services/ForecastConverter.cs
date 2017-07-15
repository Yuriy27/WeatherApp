using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheatherForecast.Models;
using WheatherForecast.Models.OpenWeatherModels;

namespace WheatherForecast.Services
{
    public class ForecastConverter : IForecastConverter
    {
        public ForecastEntity ConvertToEntity(ForecastObject obj)
        {
            var entity = new ForecastEntity();
            var temp = obj.list[0];
            entity.City = obj.City.Name;
            entity.Description = temp.Weather[0].Description;
            entity.Humidity = temp.Humidity;
            entity.Icon = temp.Weather[0].Icon;
            entity.Pressure = temp.Pressure;
            entity.TemperatureDay = temp.Temp.Day;
            entity.TemperatureEvening = temp.Temp.Eve;
            entity.TemperatureMorning = temp.Temp.Morn;
            entity.TemperatureNight = temp.Temp.Night;
            entity.WindSpeed = temp.Speed;
            entity.Date = DateTime.Now;

            return entity;
        }
    }
}