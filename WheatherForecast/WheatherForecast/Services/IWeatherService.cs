using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheatherForecast.Models;

namespace WheatherForecast.Services
{
    public interface IWeatherService
    {
        IEnumerable<CityEntity> GetCities();

        IEnumerable<CityEntity> GetCitiesByName(string name);

        IEnumerable<ForecastEntity> GetForeacsts();

        IEnumerable<string> GetCityNames();

        void AddCity(CityEntity city);

        void DeleteCity(int id);

        void UpdateCity(CityEntity city);

        void AddForecast(ForecastEntity forecast);

        IEnumerable<ForecastEntity> GetForecastsByCity(string city);

        IEnumerable<ForecastEntity> GetForecastsByDate(DateTime date);
    }
}
