using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WheatherForecast.Models;
using Newtonsoft.Json;

namespace WheatherForecast.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private string _apiKey;

        public OpenWeatherService()
        {
            _apiKey = System.Configuration.ConfigurationManager.AppSettings["AppKey"];
        }

        public ForecastObject GetForecast(string city, int days)
        {
            var uri = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&units=metric&APPID={_apiKey}&cnt={days}";
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
            request.Method = "GET";
            request.ContentType = "application/json";
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                throw;
            }
            ForecastObject forecast;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                forecast = JsonConvert.DeserializeObject<ForecastObject>(json);
            }

            return forecast;
        }
    }
}