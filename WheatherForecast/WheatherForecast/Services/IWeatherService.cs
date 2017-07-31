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
        Task<IEnumerable<CityEntity>> GetCitiesAsync();

        Task<IEnumerable<CityEntity>> GetCitiesByNameAsync(string name);
        
        Task<IEnumerable<ForecastEntity>> GetForeacstsAsync();
        
        Task<IEnumerable<string>> GetCityNamesAsync();
        
        Task AddCityAsync(CityEntity city);
        
        Task DeleteCityAsync(int id);
        
        Task UpdateCityAsync(CityEntity city);
        
        Task AddForecastAsync(ForecastEntity forecast);
        
        Task<IEnumerable<ForecastEntity>> GetForecastsByCityAsync(string city);
        
        Task<IEnumerable<ForecastEntity>> GetForecastsByDateAsync(DateTime date);
        
        Task<CityEntity> GetCityAsync(int id);
    }
}
