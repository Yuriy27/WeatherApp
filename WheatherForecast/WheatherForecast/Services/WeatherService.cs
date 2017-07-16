using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<CityEntity> GetCities()
        {
            return _uow.Repository<CityEntity>().GetAll();
        }

        public IEnumerable<CityEntity> GetCitiesByName(string name)
        {
            return _uow.Repository<CityEntity>().GetAll().Where(c => c.Name.Equals(name));
        }

        public IEnumerable<ForecastEntity> GetForeacsts()
        {
            return _uow.Repository<ForecastEntity>().GetAll();
        }

        public IEnumerable<string> GetCityNames()
        {
            return _uow.Repository<CityEntity>().GetAll()
                .Select(c => c.Name);
        }

        public void AddCity(CityEntity city)
        {
            _uow.Repository<CityEntity>().Create(city);
            _uow.Save();
        }

        public void DeleteCity(int id)
        {
            _uow.Repository<CityEntity>().Delete(id);
            _uow.Save();
        }

        public void UpdateCity(CityEntity city)
        {
            _uow.Repository<CityEntity>().Update(city);
            _uow.Save();
        }

        public void AddForecast(ForecastEntity forecast)
        {
            _uow.Repository<ForecastEntity>().Create(forecast);
            _uow.Save();
        }

        public IEnumerable<ForecastEntity> GetForecastsByCity(string city)
        {
            return _uow.Repository<ForecastEntity>().GetAll()
                .Where(f => f.City == city);
        }

        public IEnumerable<ForecastEntity> GetForecastsByDate(DateTime date)
        {
            return _uow.Repository<ForecastEntity>().GetAll()
                .Where(f => f.Date.ToShortDateString().Equals(date.ToShortDateString()));
        }

        public CityEntity GetCity(int id)
        {
            return _uow.Repository<CityEntity>().Get(id);
        }
    }
}