﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WheatherForecast.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WheatherForecast.Models.OpenWeatherModels;

namespace WheatherForecast.Services
{
    public class OpenWeatherMapProvider : IForecastProvider
    {
        private string _apiKey = "6ed509e8614438c14cac139dfa12a323";

        public async Task<ForecastObject> GetForecastAsync(string city, int days)
        {
            if (city == null)
            {
                throw new ArgumentException("Param city cannot be null");
            }
            if (days < 1 || days > 17)
            {
                throw new ArgumentException("Param days should be in range 1 to 17");
            }
            var uri = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&units=metric&APPID={_apiKey}&cnt={days}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return await Task.Run(() => JsonConvert.DeserializeObject<ForecastObject>(json));
            }
        }

        public async Task<bool> SuccessPingCityAsync(string city)
        {
            try
            {
                await GetForecastAsync(city, 1);
            }
            catch (WebException)
            {
                return false;
            }
            return true;
        }
    }
}