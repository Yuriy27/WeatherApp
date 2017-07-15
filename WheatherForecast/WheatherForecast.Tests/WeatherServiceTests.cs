using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using WheatherForecast.Models;
using WheatherForecast.Repositories.Interfaces;
using WheatherForecast.Services;

namespace WheatherForecast.Tests
{
    [TestFixture]
    class WeatherServiceTests
    {
        private IWeatherService _weatherService;

        [SetUp]
        public void Init()
        {
           
        }

        [TearDown]
        public void ShutDown()
        {
            
        }

        [Test]
        public void GetCityNames_When_city_entities_Then_city_names_correctly_returned()
        {
            var cityRepository = A.Fake<IRepository<CityEntity>>();
            A.CallTo(() => cityRepository.GetAll()).Returns(new[]
            {
                new CityEntity {Id = 1, Name = "city 1"},
                new CityEntity {Id = 2, Name = "city 2"},
                new CityEntity {Id = 3, Name = "city 3"}
            }.AsEnumerable());

            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.Repository<CityEntity>()).Returns(cityRepository);

            var weatherService = new WeatherService(uow);

            var expectedResult = new List<string> {"city 1", "city 2", "city 3"};
            var gainedResult = weatherService.GetCityNames();

            CollectionAssert.AreEqual(expectedResult, gainedResult);
        }

        [Test]
        public void GetCitiesByName_When_given_name_Then_cities_correctly_returned()
        {
            var cityRepository = A.Fake<IRepository<CityEntity>>();
            A.CallTo(() => cityRepository.GetAll()).Returns(new []
            {
                new CityEntity {Id = 1, Name = "city 1"},
                new CityEntity {Id = 2, Name = "city 2"},
                new CityEntity {Id = 3, Name = "city 2"},
                new CityEntity {Id = 4, Name = "city 2"},
                new CityEntity {Id = 5, Name = "city 2"},
                new CityEntity {Id = 6, Name = "city 3"}
            });

            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.Repository<CityEntity>()).Returns(cityRepository);

            var weatherService = new WeatherService(uow);
            var searchCity = "city 2";

            var expectedResult = new []
            {
                new CityEntity {Id = 2, Name = "city 2"},
                new CityEntity {Id = 3, Name = "city 2"},
                new CityEntity {Id = 4, Name = "city 2"},
                new CityEntity {Id = 5, Name = "city 2"}
            };
            var gainedResult = weatherService.GetCitiesByName(searchCity);

            CollectionAssert.AreEqual(expectedResult, gainedResult);
        }

        [Test]
        public void GetForecastsByCity_When_given_city_Then_forecasts_correctly_returned()
        {
            var forecastRepository = A.Fake<IRepository<ForecastEntity>>();
            A.CallTo(() => forecastRepository.GetAll()).Returns(new []
            {
                new ForecastEntity {Id = 1, City = "city 1"},
                new ForecastEntity {Id = 2, City = "city 2"},
                new ForecastEntity {Id = 3, City = "city 2"},
                new ForecastEntity {Id = 4, City = "city 2"},
                new ForecastEntity {Id = 5, City = "city 3"},
                new ForecastEntity {Id = 6, City = "city 3"}
            });

            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.Repository<ForecastEntity>()).Returns(forecastRepository);

            var weatherService = new WeatherService(uow);
            var searchCity = "city 3";

            var expectedResult = new []
            {
                new ForecastEntity {Id = 5, City = "city 3"},
                new ForecastEntity {Id = 6, City = "city 3"}
            };
            var gainedResult = weatherService.GetForecastsByCity(searchCity);

            CollectionAssert.AreEqual(expectedResult, gainedResult);
        }

        [Test]
        public void GetForecastsByDate_When_given_date_Then_cities_correctly_returned()
        {
            var forecastRepository = A.Fake<IRepository<ForecastEntity>>();
            A.CallTo(() => forecastRepository.GetAll()).Returns(new[]
            {
                new ForecastEntity {Id = 1, Date = new DateTime(2017, 7, 12)},
                new ForecastEntity {Id = 2, Date = new DateTime(2017, 7, 11)},
                new ForecastEntity {Id = 3, Date = new DateTime(2017, 7, 11)},
                new ForecastEntity {Id = 4, Date = new DateTime(2017, 7, 10)},
                new ForecastEntity {Id = 5, Date = new DateTime(2017, 7, 8)},
                new ForecastEntity {Id = 6, Date = new DateTime(2017, 7, 9)}
            });

            var uow = A.Fake<IUnitOfWork>();
            A.CallTo(() => uow.Repository<ForecastEntity>()).Returns(forecastRepository);

            var weatherService = new WeatherService(uow);
            var searchDate = new DateTime(2017, 7, 10);

            var expectedResult = new[]
            {
                new ForecastEntity {Id = 4, Date = new DateTime(2017, 7, 10)}
            };
            var gainedResult = weatherService.GetForecastsByDate(searchDate);

            CollectionAssert.AreEqual(expectedResult, gainedResult);
        }

        [Test]
        public void AddCity_When_adding_city_Then_Save_of_UnitOfWork_called()
        {
            
        }
    }
}
