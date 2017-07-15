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

        protected bool Equals(ForecastEntity other)
        {
            return Id == other.Id 
                && string.Equals(City, other.City) 
                && Date.Equals(other.Date) 
                && Pressure.Equals(other.Pressure)
                && Humidity.Equals(other.Humidity)
                && TemperatureMorning.Equals(other.TemperatureMorning) 
                && TemperatureDay.Equals(other.TemperatureDay) 
                && TemperatureEvening.Equals(other.TemperatureEvening) 
                && TemperatureNight.Equals(other.TemperatureNight) 
                && WindSpeed.Equals(other.WindSpeed) 
                && string.Equals(Description, other.Description) 
                && string.Equals(Icon, other.Icon);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ForecastEntity) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (City != null ? City.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Date.GetHashCode();
                hashCode = (hashCode * 397) ^ Pressure.GetHashCode();
                hashCode = (hashCode * 397) ^ Humidity.GetHashCode();
                hashCode = (hashCode * 397) ^ TemperatureMorning.GetHashCode();
                hashCode = (hashCode * 397) ^ TemperatureDay.GetHashCode();
                hashCode = (hashCode * 397) ^ TemperatureEvening.GetHashCode();
                hashCode = (hashCode * 397) ^ TemperatureNight.GetHashCode();
                hashCode = (hashCode * 397) ^ WindSpeed.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Icon != null ? Icon.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}