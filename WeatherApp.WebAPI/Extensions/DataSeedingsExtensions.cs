using WeatherApp.Persistence.Seedings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherApp.WebAPI.Extensions
{

    public static class DataSeedingsExtensions
    {
        public static void DataSeedings(this IServiceScope scope)
        {
            var countryDataSeeder = scope.ServiceProvider.GetService<CountryDataSeeder>();
            countryDataSeeder.Seed();

            var cityDataSeeder = scope.ServiceProvider.GetService<CityDataSeeder>();
            cityDataSeeder.Seed();
        }
    }
}