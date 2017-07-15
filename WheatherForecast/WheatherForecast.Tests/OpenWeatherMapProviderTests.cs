using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WheatherForecast.Services;

namespace WheatherForecast.Tests
{
    [TestFixture]
    class OpenWeatherMapProviderTests
    {
        private IForecastProvider _provider;

        [SetUp]
        public void Init()
        {
            _provider = new OpenWeatherMapProvider();
        }

        [TearDown]
        public void ShutDown()
        {
            
        }

        [Test]
        public void GetForecast_When_given_invalid_city_name_Then_throws_exception()
        {
            var invalidCityName = "K";
            var days = 1;

            Assert.Throws<WebException>(() => _provider.GetForecast(invalidCityName, days));
        }

        [Test]
        [TestCase(1)]
        [TestCase(17)]
        public void GetForecast_When_given_valid_city_and_positive_count_of_days_Then_result_not_null(int days)
        {
            var cityName = "Odessa";

            var result = _provider.GetForecast(cityName, days);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.City.Name, Is.EqualTo(cityName));
        }

        [Test]
        [TestCase(0)]
        [TestCase(18)]
        public void GetForecast_When_given_invalid_count_of_days_Then_throws_exception(int days)//valid days [1..17]
        {
            var cityName = "Kharkiv";

            Assert.Throws<ArgumentException>(() => _provider.GetForecast(cityName, days));
        }

        [Test]
        public void GetForecast_When_given_null_city_name_Then_throws_exception()
        {
            string cityName = null;
            int days = 7;

            Assert.Throws<ArgumentException>(() => _provider.GetForecast(cityName, days));
        }
    }
}
