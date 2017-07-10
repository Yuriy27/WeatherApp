using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WheatherForecast.Models
{
    public class ForecastContext : DbContext
    {
        public ForecastContext() : base("ForecastContext") { }

        public DbSet<CityEntity> Cities;

        public DbSet<ForecastEntity> Forecasts;
    }
}