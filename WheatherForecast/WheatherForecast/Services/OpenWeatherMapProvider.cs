using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WheatherForecast.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WheatherForecast.Services
{
    public class OpenWeatherMapProvider : IForecastProvider
    {
        private string _apiKey;

        public OpenWeatherMapProvider()
        {
            _apiKey = System.Configuration.ConfigurationManager.AppSettings["AppKey"];
        }

        public ForecastObject GetForecast(string city, int days)
        {
            var uri = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&units=metric&APPID={_apiKey}&cnt={days}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
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

        //public async Task<ForecastObject> GetForecast(string city, int days)
        //{
        //    var uri = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&units=metric&APPID={_apiKey}&cnt={days}";
        //    var httpClient = new HttpClient();
        //    var response = await httpClient.GetAsync(uri);
        //    try
        //    {
        //        response.EnsureSuccessStatusCode();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return null;
        //}
    }
}