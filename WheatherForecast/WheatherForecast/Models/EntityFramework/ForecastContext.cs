using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WheatherForecast.Models
{
    public class ForecastContext : DbContext
    {
        public ForecastContext() : base("ForecastContext")
        {
            Database.SetInitializer(new ForecastDatabaseInitializer());
        }

        public DbSet<CityEntity> Cities { get; set; }

        public DbSet<ForecastEntity> Forecasts { get; set; }
    }
}