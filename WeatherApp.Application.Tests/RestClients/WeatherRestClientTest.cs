using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Shouldly;
using WeatherApp.Application.Common.Exceptions;
using WeatherApp.Application.RestClients;
using WeatherApp.Application.Tests.Mocks.RestClients;
using WeatherApp.Domain.ResponseDtos.WeatherRestClientResponse;
using Xunit;

namespace WeatherApp.Application.Tests.RestClients
{
    public class WeatherRestClientTest
    {
        private readonly Mock<IWeatherRestClient> _mockRestClient;

        public WeatherRestClientTest()
        {
            _mockRestClient = MockWeatherRestClient.GetMockWeatherRestClient();
        }

        [Fact]
        public async Task Can_GetWeatherRestClient_Correct_Location_200_Test()
        {
            var countryCode = "AU";
            var cityName = "Sydney";
            var query = $"{cityName},{countryCode}";
            
            WeatherRestClientResponseDto result = await _mockRestClient.Object.GetByQuery(query, new CancellationToken());
            result.ShouldBeOfType<WeatherRestClientResponseDto>();
            result.Name.ShouldBe("Sydney");
            result.Sys.Country.ShouldBe("AU");
        }
        
        [Fact]
        public async Task Can_GetWeatherRestClient_Correct_Conversion_Celcius_200_Test()
        {
            var countryCode = "AU";
            var cityName = "Sydney";
            var query = $"{cityName},{countryCode}";
            double tempFahrenheit = 296.17;
            double tempCelcius = (tempFahrenheit - 32) * 5 / 9;
            // double tempCelcius = 146.76111111111112d;
            
            WeatherRestClientResponseDto result = await _mockRestClient.Object.GetByQuery(query, new CancellationToken());
            result.ShouldBeOfType<WeatherRestClientResponseDto>();
            result.Main.Temp.ShouldBe(tempFahrenheit);
            result.Main.TempCelcius.ShouldBe(tempCelcius);
        }
        
        [Fact]
        public async Task Can_GetWeatherRestClient_InCorrect_Location_404_Test()
        {
            var countryCode = "PM";
            var cityName = "Planet Mars";
            var query = $"{cityName},{countryCode}";
    
            var ex = Assert.ThrowsAsync<DynamicException>(async () => await _mockRestClient.Object.GetByQuery(query, new CancellationToken()));
            ex.Result.GetStatusCode().ShouldBe(404);

        }
        
        [Fact]
        public async Task Can_GetWeatherRestClient_Error_500_Test()
        {
            var countryCode = "ER";
            var cityName = "ERROR";
            var query = $"{cityName},{countryCode}";
            
            var ex = Assert.ThrowsAsync<Exception>(async () => await _mockRestClient.Object.GetByQuery(query, new CancellationToken()));
            var exceptionType = typeof(Exception);
            Assert.NotNull(ex.Result);
            Assert.IsType(exceptionType, ex.Result);
        }
        
        [Fact]
        public async Task Can_GetWeatherRestClient_QuotaLimit_401_Test()
        {
            var countryCode = "WK";
            var cityName = "WRONG_API_KEY";
            var query = $"{cityName},{countryCode}";
            
            var ex = Assert.ThrowsAsync<DynamicException>(async () => await _mockRestClient.Object.GetByQuery(query, new CancellationToken()));
            ex.Result.GetStatusCode().ShouldBe(401);
        }
    }
}