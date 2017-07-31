using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Ninject;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Interfaces;

namespace WheatherForecast.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IUnitOfWork _uow;

        public WeatherService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IEnumerable<CityEntity>> GetCitiesAsync()
        {
            return Task.Run(() => _uow.Repository<CityEntity>().GetAll());
        }

        public Task<IEnumerable<CityEntity>> GetCitiesByNameAsync(string name)
        {
            return Task.Run(() => _uow.Repository<CityEntity>().GetAll().Where(c => c.Name.Equals(name)));
        }

        public Task<IEnumerable<ForecastEntity>> GetForeacstsAsync()
        {
            return Task.Run(() => _uow.Repository<ForecastEntity>().GetAll());
        }

        public Task<IEnumerable<string>> GetCityNamesAsync()
        {
            return Task.Run(() => _uow.Repository<CityEntity>().GetAll()
                .Select(c => c.Name));
        }

        public async Task AddCityAsync(CityEntity city)
        {
            await Task.Run(() =>
            {
                _uow.Repository<CityEntity>().Create(city);
                _uow.Save();
            });
        }

        public async Task DeleteCityAsync(int id)
        {
            await Task.Run(() =>
            {
                _uow.Repository<CityEntity>().Delete(id);
                _uow.Save();
            });
        }

        public async Task UpdateCityAsync(CityEntity city)
        {
            await Task.Run(() =>
            {
                _uow.Repository<CityEntity>().Update(city);
                _uow.Save();
            });
        }

        public async Task AddForecastAsync(ForecastEntity forecast)
        {
            await Task.Run(() =>
            {
                _uow.Repository<ForecastEntity>().Create(forecast);
                _uow.Save();
            });
        }

        public Task<IEnumerable<ForecastEntity>> GetForecastsByCityAsync(string city)
        {
            return Task.Run(() => _uow.Repository<ForecastEntity>().GetAll()
                .Where(f => f.City == city));
        }

        public Task<IEnumerable<ForecastEntity>> GetForecastsByDateAsync(DateTime date)
        {
            return Task.Run(() =>_uow.Repository<ForecastEntity>().GetAll()
                .Where(f => f.Date.ToShortDateString().Equals(date.ToShortDateString())));
        }

        public Task<CityEntity> GetCityAsync(int id)
        {
            return Task.Run(() => _uow.Repository<CityEntity>().Get(id));
        }
    }
}