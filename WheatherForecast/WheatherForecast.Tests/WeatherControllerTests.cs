using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WheatherForecast.Controllers;
using WheatherForecast.Models;
using WheatherForecast.Models.OpenWeatherModels;
using WheatherForecast.Repositories.Concrete;
using WheatherForecast.Services;
using List = WheatherForecast.Models.List;

namespace WheatherForecast.Tests
{
    [TestFixture()]
    class WeatherControllerTests
    {
        public WeatherController GetController()
        {
            return new WeatherController(new OpenWeatherMapProvider()
                , new WeatherService(new UnitOfWork())
                , new ForecastConverter());
        }

        [Test]
        public void Index_should_return_default_view()
        {
            using (var controller = GetController())
            {
                var result = controller.Index() as ViewResult;

                Assert.NotNull(result);
                Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName.Equals("Index"));
            }
        }

        [Test]
        public void Index_When_city_null_Then_services_sholdnt_be_called()
        {
            var weatherService = A.Fake<IWeatherService>();
            var forecastProvider = A.Fake<IForecastProvider>();
            var forecastConverter = A.Fake<IForecastConverter>();

            string city = null;
            int days = 1;

            using (var controller = new WeatherController(forecastProvider, weatherService, forecastConverter))
            {
                var result = controller.Index(city, days).Result as ViewResult;

                Assert.NotNull(result);
                A.CallTo(() => weatherService.AddForecastAsync(A<ForecastEntity>._)).MustHaveHappened(Repeated.Never);
                A.CallTo(() => forecastProvider.GetForecastAsync(city, days)).MustHaveHappened(Repeated.Never);
            }
        }

        [Test]
        public void Index_When_city_kiev_days_1_Then_services_should_be_called_once()
        {
            var weatherService = A.Fake<IWeatherService>();
            var forecastProvider = A.Fake<IForecastProvider>();
            var forecastConverter = A.Fake<IForecastConverter>();

            string city = "Kiev";
            int days = 1;

            A.CallTo(() => forecastProvider.GetForecastAsync(A<string>._, A<int>._)).Returns(new ForecastObject
            {
                City = new City()
                {
                    Name = city
                }               
            });
            A.CallTo(() => forecastConverter.ConvertToEntity(A<ForecastObject>._)).Returns(new ForecastEntity()
            {
                City = city
            });

            
            using (var controller = new WeatherController(forecastProvider, weatherService, forecastConverter))
            {
                var result = controller.Index(city, days).Result as ViewResult;
                var model = result.Model;

                A.CallTo(() => weatherService.AddForecastAsync(A<ForecastEntity>._)).MustHaveHappened(Repeated.Exactly.Once);
                A.CallTo(() => forecastProvider.GetForecastAsync(city, days)).MustHaveHappened(Repeated.Exactly.Once);
                Assert.NotNull(result);
                Assert.NotNull(model);
            }
        }

        [Test]
        public void Index_When_city_kiev_days_20_Then_throws_exception()
        {
            var city = "Kiev";
            int days = 20;

            using (var controller = GetController())
            {
                Assert.Throws<ArgumentException>(() => controller.Index(city, days));
            }
        }
    }
}
