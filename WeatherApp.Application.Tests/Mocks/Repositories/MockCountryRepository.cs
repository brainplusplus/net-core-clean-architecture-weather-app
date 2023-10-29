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

    public class MockCountryRepository
    {
        public static Guid GetOneOfGuid()
        {
            return Guid.Parse("63559BC0-1FEF-4158-968E-AE4B94974F8E");
        }

        public static Mock<ICountryRepository> GetMockCountryRepository()
        {
            var countries = new List<Country>()
            {
                new Country()
                {
                    Id = GetOneOfGuid(),
                    Code = "AU",
                    Name = "Australia",
                },
                new Country()
                {
                    Id = Guid.NewGuid(),
                    Code = "ID",
                    Name = "Indonesia",
                }
            };

            var mockRepo = new Mock<ICountryRepository>();

            mockRepo.Setup(r => r.GetAll(It.IsAny<CancellationToken>())).Returns(Task.FromResult(countries));

            mockRepo.Setup(r => r.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(
                (Guid id, CancellationToken token) =>
                {
                    
                    return Task.FromResult(countries.FirstOrDefault(x => x.Id == id));
                });

            mockRepo.Setup(r => r.GetByCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(
                (string code, CancellationToken token) =>
                {
                    return Task.FromResult(countries.FirstOrDefault(x => x.Code == code));
                });

            mockRepo.Setup(r => r.Exists(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(
                (Guid id, CancellationToken token) =>
                {
                    return Task.FromResult(countries.FirstOrDefault(x => x.Id == id) != null);
                });

            mockRepo.Setup(r => r.Add(It.IsAny<Country>())).Returns((Country country) =>
            {
                DateTime now = DateTime.Now;
                country.Id = Guid.NewGuid();
                country.DateCreated = now;
                country.DateModified = now;
                countries.Add(country);
                return Task.FromResult(country);
                // return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<Country>())).Returns((Country country) =>
            {
                DateTime now = DateTime.Now;
                var row = countries.FirstOrDefault(x => x.Id == country.Id);
                if (row == null)
                {
                    // return Task.FromResult(false);
                    return Task.CompletedTask;
                }

                row.Name = country.Name;
                row.Code = country.Code;
                row.DateModified = now;
                // return Task.FromResult(true);
                return Task.CompletedTask;
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<Country>())).Returns((Country country) =>
            {
                var row = countries.FirstOrDefault(x => x.Id == country.Id);
                if (row == null)
                {
                    return Task.FromResult(false);
                }

                countries.Remove(row);
                return Task.FromResult(true);
            });


            return mockRepo;
        }
    }
}