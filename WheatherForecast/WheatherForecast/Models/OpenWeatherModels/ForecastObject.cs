using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WheatherForecast.Models
{
    public class ForecastObject
    {
        public City City { get; set; }
        public string Cod { get; set; }
        public double Message { get; set; }
        public int Cnt { get; set; }
        public List<List> list { get; set; }

        public static explicit operator ForecastEntity(ForecastObject obj)
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