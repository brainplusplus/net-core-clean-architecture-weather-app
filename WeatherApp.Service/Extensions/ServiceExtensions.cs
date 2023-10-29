using WeatherApp.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Application.Common.Repositories;
using WeatherApp.Application.RestClients;
using WeatherApp.Service.RestClients;

namespace WeatherApp.Service.Extensions
{

    public static class ServiceExtensions
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            var configOpenWeatherMap = configuration.GetSection("OpenWeatherMap");
            var baseAddress = configOpenWeatherMap["BaseApiURL"];
            var apiKey = configOpenWeatherMap["ApiKey"];

            services.AddSingleton<IWeatherRestClient>(factory => new WeatherRestClient(baseAddress, apiKey));
        }
    }
}