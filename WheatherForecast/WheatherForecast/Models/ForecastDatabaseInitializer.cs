using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WheatherForecast.Models
{
    public class ForecastDatabaseInitializer : CreateDatabaseIfNotExists<ForecastContext>
    {
        protected override void Seed(ForecastContext context)
        {
            List<string> cityNames = new List<string>() { "Lviv", "Kiev", "Dnipropetrovsk", "Kharkiv", "Odessa" };
            foreach (string name in cityNames)
            {
                context.Cities.Add(new CityEntity { Name = name});
            }
            base.Seed(context);
        }
    }
}