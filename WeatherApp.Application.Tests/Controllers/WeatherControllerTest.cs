using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Shouldly;
using WeatherApp.Application.Common.Exceptions;
using WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery;
using WeatherApp.Application.Features.WeatherFeatures.GetWeatherByLocationQuery;
using WeatherApp.Application.Tests.Mocks.Common;
using Xunit;

namespace WeatherApp.Application.Tests.Controllers
{
    public class WeatherControllerTest
    {
        private readonly Mock<IMediator> _mediator;

        public WeatherControllerTest()
        {
            _mediator = MockMediator.GetMockMediator();
        }

        [Fact]
        public async Task Can_GetByLocation_Weather_Controller_200_Test()
        {
            var countryCode = "AU";
            var cityName = "Sydney";
            var query = $"{cityName},{countryCode}";
            var response = await _mediator.Object.Send(new GetWeatherByLocationRequest{Q=query}, CancellationToken.None);

            response.ShouldBeOfType<GetWeatherByLocationResponse>();
        }
        
        [Fact]
        public async Task Can_GetByLocation_Correct_Conversion_Celcius_Weather_Controller_200_Test()
        {
            var countryCode = "AU";
            var cityName = "Sydney";
            var query = $"{cityName},{countryCode}";
            double tempFahrenheit = 296.17;
            double tempCelcius = (tempFahrenheit - 32) * 5 / 9;
            // double tempCelcius = 146.76111111111112d;
            
            var response = await _mediator.Object.Send(new GetWeatherByLocationRequest{Q=query}, CancellationToken.None);

            response.ShouldBeOfType<GetWeatherByLocationResponse>();
            response.TemperatureFahrenheit.ShouldBe(tempFahrenheit);
            response.TemperatureCelcius.ShouldBe(tempCelcius);
        }
        
        [Fact]
        public async Task Can_GetByLocation_Weather_Controller_404_Test()
        {
            var countryCode = "US";
            var cityName = "Planet Mars";
            var query = $"{cityName},{countryCode}";
            
            var ex = Assert.ThrowsAsync<DynamicException>(async () => await _mediator.Object.Send(new GetWeatherByLocationRequest{Q=query}, CancellationToken.None));
            ex.Result.GetStatusCode().ShouldBe(404);
        }
        
        [Fact]
        public async Task Can_GetByLocation_Weather_Controller_500_Test()
        {
            var countryCode = "ER";
            var cityName = "ERROR";
            var query = $"{cityName},{countryCode}";
            
            var ex = Assert.ThrowsAsync<Exception>(async () => await _mediator.Object.Send(new GetWeatherByLocationRequest{Q=query}, CancellationToken.None));
            var exceptionType = typeof(Exception);
            Assert.NotNull(ex.Result);
            Assert.IsType(exceptionType, ex.Result);
        }
        
        [Fact]
        public async Task Can_GetByLocation_Weather_Controller_401_Test()
        {
            var countryCode = "WK";
            var cityName = "WRONG_API_KEY";
            var query = $"{cityName},{countryCode}";
            
            var ex = Assert.ThrowsAsync<DynamicException>(async () => await _mediator.Object.Send(new GetWeatherByLocationRequest{Q=query}, CancellationToken.None));
            ex.Result.GetStatusCode().ShouldBe(401);
        }
    }
}