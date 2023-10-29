using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Repositories;
using WeatherApp.Domain.Entities;
using System.Linq;

namespace WeatherApp.Application.Tests.Mocks.Repositories
{

    public class MockCityRepository
    {
        public static Guid GetOneOfGuid()
        {
            return Guid.Parse("63559BC0-1FEF-4158-968E-AE4B94974F8E");
        }

        public static Guid GetOneOfCountryGuid()
        {
            return Guid.Parse("96e51dfe-f535-4502-850f-99287ac8e2c6");
        }

        public static Country GetOneOfCountry()
        {
            return new Country()
            {
                Id = GetOneOfCountryGuid(),
                Code = "ID",
                Name = "Indonesia",
            };
        }

        public static Mock<ICityRepository> GetMockCityRepository()
        {
            var countryID = GetOneOfCountry();
            List<City> citiesID = new List<City>()
            {
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryID,
                    Name = "Bekasi",
                    Lat = -6.2349,
                    Lon = 106.9896
                },
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryID,
                    Name = "Bogor",
                    Lat = -6.5944,
                    Lon = 106.7892
                },
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryID,
                    Name = "Depok",
                    Lat = -6.402484,
                    Lon = 106.794243
                },
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryID,
                    Name = "Jakarta",
                    Lat = -6.2146,
                    Lon = 106.8451
                },
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryID,
                    Name = "Tangerang",
                    Lat = -6.1781,
                    Lon = 106.63
                },
                new City()
                {
                    Id = GetOneOfGuid(),
                    Country = countryID,
                    Name = "Tangerang Selatan",
                    Lat = -6.326740,
                    Lon = 106.730042
                }
            };

            var countryAU = new Country()
            {
                Id = Guid.NewGuid(),
                Code = "AU",
                Name = "Australia",
            };
            List<City> citiesAU = new List<City>()
            {
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryAU,
                    Name = "Adelaide",
                    Lat = -34.928497,
                    Lon = 138.600739
                },
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryAU,
                    Name = "Brisbane",
                    Lat = -27.4679,
                    Lon = 153.0281
                },
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryAU,
                    Name = "Canberra",
                    Lat = -35.2835,
                    Lon = 149.1281
                },
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryAU,
                    Name = "Darwin",
                    Lat = -12.4611,
                    Lon = 130.8418
                },
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryAU,
                    Name = "Perth",
                    Lat = -31.9333,
                    Lon = 115.8333
                },
                new City()
                {
                    Id = Guid.NewGuid(),
                    Country = countryAU,
                    Name = "Sydney",
                    Lat = -33.8679,
                    Lon = 151.2073
                }
            };
            var cities = citiesAU.Concat(citiesID).ToList();

            var mockRepo = new Mock<ICityRepository>();

            mockRepo.Setup(r => r.GetAll(It.IsAny<CancellationToken>())).Returns(Task.FromResult(cities));

            mockRepo.Setup(r => r.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(
                (Guid id, CancellationToken token) =>
                {
                    return Task.FromResult(cities.FirstOrDefault(x => x.Id == id));
                });

            mockRepo.Setup(r => r.GetAllByCountryCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(
                (string code, CancellationToken token) =>
                {
                    return Task.FromResult(cities.Where(x => x.Country.Code == code).ToList());
                });

            mockRepo.Setup(r => r.Exists(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(
                (Guid id, CancellationToken token) =>
                {
                    return Task.FromResult(cities.FirstOrDefault(x => x.Id == id) != null);
                });

            mockRepo.Setup(r => r.Add(It.IsAny<City>())).Returns((City city) =>
            {
                DateTime now = DateTime.Now;
                city.Id = Guid.NewGuid();
                city.DateCreated = now;
                city.DateModified = now;
                cities.Add(city);
                return Task.FromResult(city);
                // return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<City>())).Returns((City city) =>
            {
                DateTime now = DateTime.Now;
                var row = cities.FirstOrDefault(x => x.Id == city.Id);
                if (row == null)
                {
                    // return Task.FromResult(false);
                    return Task.CompletedTask;
                }

                row.Name = city.Name;
                row.Country = city.Country;
                row.Lat = city.Lat;
                row.Lon = city.Lon;
                row.DateModified = now;
                // return Task.FromResult(true);
                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<City>())).Returns((City city) =>
            {
                var row = cities.FirstOrDefault(x => x.Id == city.Id);
                if (row == null)
                {
                    return Task.FromResult(false);
                }

                cities.Remove(row);
                return Task.FromResult(true);
            });


            return mockRepo;
        }
    }
}