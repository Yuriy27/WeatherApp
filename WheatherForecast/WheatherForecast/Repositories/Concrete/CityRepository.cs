using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Interfaces;

namespace WheatherForecast.Repositories.Concrete
{
    public class CityRepository : IRepository<CityEntity>
    {
        private ForecastContext _context;

        public CityRepository(ForecastContext context)
        {
            _context = context;
        }

        public void Create(CityEntity item)
        {
            _context.Cities.Add(item);
        }

        public void Delete(int id)
        {
            var city = _context.Cities.Find(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
            }
        }

        public CityEntity Get(int id)
        {
            return _context.Cities.Find(id);
        }

        public IEnumerable<CityEntity> GetAll()
        {
            return _context.Cities;
        }

        public void Update(CityEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}