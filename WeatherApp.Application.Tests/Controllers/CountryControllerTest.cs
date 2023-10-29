using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Shouldly;
using WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery;
using WeatherApp.Application.Tests.Mocks.Common;
using Xunit;

namespace WeatherApp.Application.Tests.Controllers
{
    public class CountryControllerTest
    {
        private readonly Mock<IMediator> _mediator;

        public CountryControllerTest()
        {
            _mediator = MockMediator.GetMockMediator();
        }

        [Fact]
        public async Task Can_GetAll_Country_Controller_Test()
        {
            var response = await _mediator.Object.Send(new GetAllCountryRequest(), CancellationToken.None);

            response.ShouldBeOfType<List<GetAllCountryResponse>>();
            response.Count.ShouldBe(2);
        }
    }
}