using Xunit;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Repositories;
using WeatherApp.Application.Tests.Mocks.Repositories;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Tests.Repositories
{

    public class CityRepositoryTest
    {
        private readonly Mock<ICityRepository> _mockRepo;

        public CityRepositoryTest()
        {
            _mockRepo = MockCityRepository.GetMockCityRepository();
        }

        [Fact]
        public async Task Can_GetAll_City_Test()
        {
            List<City> cities = await _mockRepo.Object.GetAll(new CancellationToken());
            cities.ShouldBeOfType<List<City>>();
            cities.Count.ShouldBe(12);
        }

        [Fact]
        public async Task Can_GetById_City_Test()
        {
            Guid cityId = MockCityRepository.GetOneOfGuid();
            City city = await _mockRepo.Object.Get(cityId, new CancellationToken());
            city.ShouldBeOfType<City>();
            city.Name.ShouldBe("Tangerang Selatan");

            Guid notFoundCityId = Guid.NewGuid();
            City notFoundCity = await _mockRepo.Object.Get(notFoundCityId, new CancellationToken());
            notFoundCity.ShouldBeNull();
        }

        [Fact]
        public async Task Can_GetAllByCountryCode_City_Test()
        {
            string countryCode = "AU";
            List<City> cities = await _mockRepo.Object.GetAllByCountryCode(countryCode, new CancellationToken());
            cities.ShouldBeOfType<List<City>>();
            cities.Count.ShouldBe(6);

            string notFoundCountryCode = "JP";
            List<City> notFoundCities =
                await _mockRepo.Object.GetAllByCountryCode(notFoundCountryCode, new CancellationToken());
            notFoundCities.Count.ShouldBe(0);
        }

        [Fact]
        public async Task Can_Exists_City_Test()
        {
            Guid cityId = MockCityRepository.GetOneOfGuid();
            bool isFoundCity = await _mockRepo.Object.Exists(cityId, new CancellationToken());
            isFoundCity.ShouldBeOfType<bool>();
            isFoundCity.ShouldBe(true);

            Guid notFoundCityId = Guid.NewGuid();
            bool isNotFoundCity = await _mockRepo.Object.Exists(notFoundCityId, new CancellationToken());
            isNotFoundCity.ShouldBeOfType<bool>();
            isNotFoundCity.ShouldBe(false);
        }

        [Fact]
        public async Task Can_Add_City_Test()
        {
            var country = MockCityRepository.GetOneOfCountry();
            var city = new City()
            {
                Country = country,
                Name = "Boyolali",
                Lat = -7.520530,
                Lon = 110.595023
            };
            var citySave = await _mockRepo.Object.Add(city);
            citySave.ShouldBeOfType<City>();
            citySave.Id.ShouldNotBe(Guid.Empty);
            citySave.Name.ShouldBe("Boyolali");
            Guid cityId = citySave.Id;

            bool isFoundCity = await _mockRepo.Object.Exists(cityId, new CancellationToken());
            isFoundCity.ShouldBeOfType<bool>();
            isFoundCity.ShouldBe(true);

            List<City> cities = await _mockRepo.Object.GetAll(new CancellationToken());
            cities.ShouldBeOfType<List<City>>();
            cities.Count.ShouldBe(13);
        }

        [Fact]
        public async Task Can_Update_City_Test()
        {
            Guid cityId = MockCityRepository.GetOneOfGuid();
            City city = await _mockRepo.Object.Get(cityId, new CancellationToken());
            city.ShouldBeOfType<City>();
            city.Name.ShouldBe("Tangerang Selatan");

            //update name
            city.Name = "Tangerang";

            //update to database
            await _mockRepo.Object.Update(city);

            //get city to check updated name
            City cityUpdated = await _mockRepo.Object.Get(cityId, new CancellationToken());
            cityUpdated.ShouldBeOfType<City>();
            cityUpdated.Name.ShouldBe("Tangerang");
        }

        [Fact]
        public async Task Can_Delete_City_Test()
        {
            Guid cityId = MockCityRepository.GetOneOfGuid();
            City city = await _mockRepo.Object.Get(cityId, new CancellationToken());
            city.ShouldBeOfType<City>();
            city.Name.ShouldBe("Tangerang Selatan");

            //delete to database
            await _mockRepo.Object.Delete(city);

            //get city to check new list
            List<City> cities = await _mockRepo.Object.GetAll(new CancellationToken());
            cities.ShouldBeOfType<List<City>>();
            cities.Count.ShouldBe(11);

            var country = MockCityRepository.GetOneOfCountry();
            var notFoundCityToDelete = new City()
            {
                Country = country,
                Name = "Boyolali",
                Lat = -7.520530,
                Lon = 110.595023
            };
            //delete to database
            await _mockRepo.Object.Delete(notFoundCityToDelete);

            //get city to check new list
            List<City> cities2 = await _mockRepo.Object.GetAll(new CancellationToken());
            cities2.ShouldBeOfType<List<City>>();
            cities2.Count.ShouldBe(11);
        }
    }
}