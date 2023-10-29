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

    public class CountryRepositoryTest
    {
        private readonly Mock<ICountryRepository> _mockRepo;

        public CountryRepositoryTest()
        {
            _mockRepo = MockCountryRepository.GetMockCountryRepository();
        }

        [Fact]
        public async Task Can_GetAll_Country_Test()
        {
            List<Country> countries = await _mockRepo.Object.GetAll(new CancellationToken());
            countries.ShouldBeOfType<List<Country>>();
            countries.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Can_GetById_Country_Test()
        {
            Guid countryId = MockCountryRepository.GetOneOfGuid();
            Country country = await _mockRepo.Object.Get(countryId, new CancellationToken());
            country.ShouldBeOfType<Country>();
            country.Code.ShouldBe("AU");

            Guid notFoundCountryId = Guid.NewGuid();
            Country notFoundCountry = await _mockRepo.Object.Get(notFoundCountryId, new CancellationToken());
            notFoundCountry.ShouldBeNull();
        }

        [Fact]
        public async Task Can_GetByCode_Country_Test()
        {
            string code = "AU";
            Country country = await _mockRepo.Object.GetByCode(code, new CancellationToken());
            country.ShouldBeOfType<Country>();
            country.Name.ShouldBe("Australia");

            string notFoundCode = "JP";
            Country notFoundCountry = await _mockRepo.Object.GetByCode(notFoundCode, new CancellationToken());
            notFoundCountry.ShouldBeNull();
        }

        [Fact]
        public async Task Can_Exists_Country_Test()
        {
            Guid countryId = MockCountryRepository.GetOneOfGuid();
            bool isFoundCountry = await _mockRepo.Object.Exists(countryId, new CancellationToken());
            isFoundCountry.ShouldBeOfType<bool>();
            isFoundCountry.ShouldBe(true);

            Guid notFoundCountryId = Guid.NewGuid();
            bool isNotFoundCountry = await _mockRepo.Object.Exists(notFoundCountryId, new CancellationToken());
            isNotFoundCountry.ShouldBeOfType<bool>();
            isNotFoundCountry.ShouldBe(false);
        }

        [Fact]
        public async Task Can_Add_Country_Test()
        {
            var country = new Country()
            {
                Code = "US",
                Name = "United States",
            };
            var countrySave = await _mockRepo.Object.Add(country);
            countrySave.ShouldBeOfType<Country>();
            countrySave.Id.ShouldNotBe(Guid.Empty);
            countrySave.Name.ShouldBe("United States");
            Guid countryId = countrySave.Id;

            bool isFoundCountry = await _mockRepo.Object.Exists(countryId, new CancellationToken());
            isFoundCountry.ShouldBeOfType<bool>();
            isFoundCountry.ShouldBe(true);

            List<Country> countries = await _mockRepo.Object.GetAll(new CancellationToken());
            countries.ShouldBeOfType<List<Country>>();
            countries.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Can_Update_Country_Test()
        {
            Guid countryId = MockCountryRepository.GetOneOfGuid();
            Country country = await _mockRepo.Object.Get(countryId, new CancellationToken());
            country.ShouldBeOfType<Country>();
            country.Code.ShouldBe("AU");

            //update name
            country.Name = "India";

            //update to database
            await _mockRepo.Object.Update(country);

            //get country to check updated name
            Country countryUpdated = await _mockRepo.Object.Get(countryId, new CancellationToken());
            countryUpdated.ShouldBeOfType<Country>();
            countryUpdated.Name.ShouldBe("India");
        }

        [Fact]
        public async Task Can_Delete_Country_Test()
        {
            Guid countryId = MockCountryRepository.GetOneOfGuid();
            Country country = await _mockRepo.Object.Get(countryId, new CancellationToken());
            country.ShouldBeOfType<Country>();
            country.Code.ShouldBe("AU");

            //delete to database
            await _mockRepo.Object.Delete(country);

            //get country to check new list
            List<Country> countries = await _mockRepo.Object.GetAll(new CancellationToken());
            countries.ShouldBeOfType<List<Country>>();
            countries.Count.ShouldBe(1);

            var notFoundCountryToDelete = new Country()
            {
                Id = Guid.NewGuid(),
                Code = "US",
                Name = "United States",
            };
            //delete to database
            await _mockRepo.Object.Delete(notFoundCountryToDelete);

            //get country to check new list
            List<Country> countries2 = await _mockRepo.Object.GetAll(new CancellationToken());
            countries2.ShouldBeOfType<List<Country>>();
            countries2.Count.ShouldBe(1);
        }
    }
}