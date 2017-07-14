using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WheatherForecast.Repositories.Interfaces;

namespace WheatherForecast.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dataSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }

        public void Create(T item)
        {
            _dataSet.Add(item);
        }

        public void Delete(int id)
        {
            var item = _dataSet.Find(id);
            if (item != null)
            {
                _dataSet.Remove(item);
            }
        }

        public T Get(int id)
        {
            return _dataSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dataSet;
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}