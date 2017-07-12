using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WheatherForecast.Models
{
    public class ForecastEntity
    {
        public int Id { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public double Pressure { get; set; }

        public double Humidity { get; set; }

        public double TemperatureMorning { get; set; }

        public double TemperatureDay { get; set; }

        public double TemperatureEvening { get; set; }

        public double TemperatureNight { get; set; }

        public double WindSpeed { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }
    }
}