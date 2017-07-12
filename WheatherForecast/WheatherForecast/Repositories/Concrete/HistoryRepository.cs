using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Interfaces;

namespace WheatherForecast.Repositories.Concrete
{
    public class HistoryRepository : IRepository<ForecastEntity>
    {
        private ForecastContext _context;

        public HistoryRepository(ForecastContext context)
        {
            _context = context;
        }

        public void Create(ForecastEntity item)
        {
            _context.Forecasts.Add(item);
        }

        public void Delete(int id)
        {
            var forecast = _context.Forecasts.Find(id);
            if (forecast != null)
            {
                _context.Forecasts.Remove(forecast);
            }
        }

        public ForecastEntity Get(int id)
        {
            return _context.Forecasts.Find(id);
        }

        public IEnumerable<ForecastEntity> GetAll()
        {
            return _context.Forecasts;
        }

        public void Update(ForecastEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}