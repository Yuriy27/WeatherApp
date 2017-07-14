using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Interfaces;

namespace WheatherForecast.Repositories.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForecastContext _context = new ForecastContext();
        //private CityRepository _cityRepository;

        //private HistoryRepository _historyRepository;

        //public CityRepository Cities
        //{
        //    get
        //    {
        //        if (_cityRepository == null)
        //        {
        //            _cityRepository = new CityRepository(_context);
        //        }
        //        return _cityRepository;
        //    }
        //}

        //public HistoryRepository Forecasts
        //{
        //    get
        //    {
        //        if (_historyRepository == null)
        //        {
        //            _historyRepository = new HistoryRepository(_context);
        //        }
        //        return _historyRepository;
        //    }
        //}

        public IRepository<T> Repository<T>() where T : class
        {
            return new Repository<T>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}