using Xunit;
using Shouldly;
using WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace WeatherApp.Application.Tests.Mocks.Features.CountryFeatures.GetAllCountryQuery
{

    public class GetAllCountryHandlerTest
    {
        private readonly GetAllCountryHandler _handler;

        public GetAllCountryHandlerTest()
        {
            _handler = MockGetAllCountryQuery.GetAllCountryHandler();
        }

        [Fact]
        public async Task Can_GetAllCountryHandler_Test()
        {
            var result = await _handler.Handle(new GetAllCountryRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<GetAllCountryResponse>>();
            result.Count.ShouldBe(2);
        }
    }
}