using WeatherApp.Persistence.Repositories;
using WeatherApp.Application.Repositories;
using WeatherApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Application.Common.Repositories;
using WeatherApp.Application.Common.Seedings;
using WeatherApp.Application.Seedings;
using WeatherApp.Persistence.Seedings;

namespace WeatherApp.Persistence.Extensions
{

    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Sqlite");
            services.AddDbContext<DataContext>(opt => opt.UseSqlite(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();

            services.AddTransient<CountryDataSeeder>();
            services.AddTransient<CityDataSeeder>();
        }
    }
}