using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using WeatherApp.Application.Common.Exceptions;
using WeatherApp.Application.Features.WeatherFeatures.GetWeatherByLocationQuery;
using WeatherApp.Application.Tests.Mocks.Features.WeatherFeatures.GetWeatherByLocationQuery;
using WeatherApp.Domain.ResponseDtos.WeatherRestClientResponse;
using Xunit;

namespace WeatherApp.Application.Tests.Features.WeatherFeatures.GetWeatherByLocationQuery
{
    public class GetWeatherByLocationHandlerTest
    {
        private readonly GetWeatherByLocationHandler _handler;

        public GetWeatherByLocationHandlerTest()
        {
            _handler = MockGetWeatherByLocationQuery.GetWeatherByLocationHandler();
        }

        [Fact]
        public async Task Can_GetWeatherHandler_Correct_Location_200_Test()
        {
            var countryCode = "AU";
            var cityName = "Sydney";
            var result = await _handler.Handle(new GetWeatherByLocationRequest{Q=$"{cityName},{countryCode}"}, CancellationToken.None);

            result.ShouldBeOfType<GetWeatherByLocationResponse>();
        }
        
        [Fact]
        public async Task Can_GetWeatherHandler_Correct_Conversion_Celcius_200_Test()
        {
            var countryCode = "AU";
            var cityName = "Sydney";
            double tempFahrenheit = 296.17;
            double tempCelcius = (tempFahrenheit - 32) * 5 / 9;
            // double tempCelcius = 146.76111111111112d;
            
            var result = await _handler.Handle(new GetWeatherByLocationRequest{Q=$"{cityName},{countryCode}"}, CancellationToken.None);

            result.ShouldBeOfType<GetWeatherByLocationResponse>();
            result.TemperatureFahrenheit.ShouldBe(tempFahrenheit);
            result.TemperatureCelcius.ShouldBe(tempCelcius);
        }
        
        [Fact]
        public async Task Can_GetWeatherHandler_InCorrect_Location_404_Test()
        {
            var countryCode = "US";
            var cityName = "Planet Mars";
            
            var ex = Assert.ThrowsAsync<DynamicException>(async () => await _handler.Handle(new GetWeatherByLocationRequest{Q=$"{cityName},{countryCode}"}, CancellationToken.None));
            ex.Result.GetStatusCode().ShouldBe(404);
        }
        
        [Fact]
        public async Task Can_GetWeatherHandler_Error_500_Test()
        {
            var countryCode = "ER";
            var cityName = "ERROR";

            var ex = Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(new GetWeatherByLocationRequest{Q=$"{cityName},{countryCode}"}, CancellationToken.None));
            var exceptionType = typeof(Exception);
            Assert.NotNull(ex.Result);
            Assert.IsType(exceptionType, ex.Result);
        }
        
        [Fact]
        public async Task Can_GetWeatherHandler_QuotaLimit_401_Test()
        {
            var countryCode = "WK";
            var cityName = "WRONG_API_KEY";

            var ex = Assert.ThrowsAsync<DynamicException>(async () => await _handler.Handle(new GetWeatherByLocationRequest{Q=$"{cityName},{countryCode}"}, CancellationToken.None));
            ex.Result.GetStatusCode().ShouldBe(401);
        }
    }
}