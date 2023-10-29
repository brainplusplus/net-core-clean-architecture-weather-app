using System;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using WeatherApp.Domain.Entities;
using WeatherApp.Persistence.Context;
using Xunit;

namespace WeatherApp.Application.Tests.Contexts
{
    public class WeatherAppDbContextTest
    {
        private DataContext _dataContext;
        
        public WeatherAppDbContextTest()
        {
            var dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _dataContext = new DataContext(dbOptions);
        }

        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            var country = new Country()
            {
                Id = Guid.NewGuid(),
                Code = "AU",
                Name = "Australia"
            };

            await _dataContext.Countries.AddAsync(country);
            await _dataContext.SaveChangesAsync();
            
            country.DateCreated.ShouldNotBeNull();
        }
        
        [Fact]
        public async void Save_SetDateModifiedValue()
        {
            var country = new Country()
            {
                Id = Guid.NewGuid(),
                Code = "AU",
                Name = "Australia"
            };

            await _dataContext.Countries.AddAsync(country);
            await _dataContext.SaveChangesAsync();
            
            country.DateModified.ShouldNotBeNull();
        }
    }
}