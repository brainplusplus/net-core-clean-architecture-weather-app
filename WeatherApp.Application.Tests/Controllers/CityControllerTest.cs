using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Shouldly;
using WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery;
using WeatherApp.Application.Tests.Mocks.Common;
using Xunit;

namespace WeatherApp.Application.Tests.Controllers
{
    public class CityControllerTest
    {
        private readonly Mock<IMediator> _mediator;

        public CityControllerTest()
        {
            _mediator = MockMediator.GetMockMediator();
        }

        [Fact]
        public async Task Can_GetAllByCountryCode_City_Controller_List_Test()
        {
            var countryCode = "ID";
            var response = await _mediator.Object.Send(new GetAllCityByCountryCodeRequest{CountryCode = countryCode}, CancellationToken.None);

            response.ShouldBeOfType<List<GetAllCityByCountryCodeResponse>>();
            response.Count.ShouldBe(6);
        }
        
        [Fact]
        public async Task Can_GetAllByCountryCode_City_Controller_Empty_Test()
        {
            var countryCode = "HL";
            var response = await _mediator.Object.Send(new GetAllCityByCountryCodeRequest{CountryCode = countryCode}, CancellationToken.None);

            response.ShouldBeOfType<List<GetAllCityByCountryCodeResponse>>();
            response.Count.ShouldBe(0);
        }
    }
}