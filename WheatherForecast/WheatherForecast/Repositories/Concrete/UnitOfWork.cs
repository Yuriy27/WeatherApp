using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheatherForecast.Models;

namespace WheatherForecast.Repositories.Concrete
{
    public class UnitOfWork : IDisposable
    {
        private ForecastContext _context = new ForecastContext();

        private CityRepository _cityRepository;

        private HistoryRepository _historyRepository;

        public CityRepository Cities
        {
            get
            {
                if (_cityRepository == null)
                {
                    _cityRepository = new CityRepository(_context);
                }
                return _cityRepository;
            }
        }

        public HistoryRepository Forecasts
        {
            get
            {
                if (_historyRepository == null)
                {
                    _historyRepository = new HistoryRepository(_context);
                }
                return _historyRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}