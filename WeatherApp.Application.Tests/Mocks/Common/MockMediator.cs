using System;
using System.Collections.Generic;
using System.Threading;
using MediatR;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery;
using WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery;
using WeatherApp.Application.Features.WeatherFeatures.GetWeatherByLocationQuery;
using WeatherApp.Application.Tests.Mocks.Features.CityFeatures.GetAllCityByCountryCodeQuery;
using WeatherApp.Application.Tests.Mocks.Features.CountryFeatures.GetAllCountryQuery;
using WeatherApp.Application.Tests.Mocks.Features.WeatherFeatures.GetWeatherByLocationQuery;

namespace WeatherApp.Application.Tests.Mocks.Common
{
    public class MockMediator
    {
        public static Mock<IMediator> GetMockMediator()
        {
            var mediator = new Mock<IMediator>();
            mediator.Setup(m => m.Send(It.IsAny<GetAllCountryRequest>(), It.IsAny<CancellationToken>())).Returns(
                async(GetAllCountryRequest request, CancellationToken cancellationToken) =>
                {
                    var _handler = MockGetAllCountryQuery.GetAllCountryHandler();
                    return await _handler.Handle(request, CancellationToken.None);
                });
            
            mediator.Setup(m => m.Send(It.IsAny<GetAllCityByCountryCodeRequest>(), It.IsAny<CancellationToken>())).Returns(
                async(GetAllCityByCountryCodeRequest request, CancellationToken cancellationToken) =>
                {
                    var _handler = MockGetAllCityByCountryCodeQuery.GetAllCityByCountryCodeHandler();
                    return await _handler.Handle(request, CancellationToken.None);
                });
            
            mediator.Setup(m => m.Send(It.IsAny<GetWeatherByLocationRequest>(), It.IsAny<CancellationToken>())).Returns(async (GetWeatherByLocationRequest request, CancellationToken cancellationToken) =>
                {
                    var _handler = MockGetWeatherByLocationQuery.GetWeatherByLocationHandler();
                    return await _handler.Handle(request, CancellationToken.None);
                });

            return mediator;
        }
    }
}