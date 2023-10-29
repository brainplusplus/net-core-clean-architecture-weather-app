using Xunit;
using Shouldly;
using WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery;
using WeatherApp.Application.Tests.Mocks.Features.CityFeatures.GetAllCityByCountryCodeQuery;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace WeatherApp.Application.Tests.Features.CityFeatures.GetAllCityByCountryCodeQuery
{

    public class GetAllCityByCountryCodeHandlerTest
    {
        private readonly GetAllCityByCountryCodeHandler _handler;

        public GetAllCityByCountryCodeHandlerTest()
        {
            _handler = MockGetAllCityByCountryCodeQuery.GetAllCityByCountryCodeHandler();
        }

        [Fact]
        public async Task Can_GetAllCountryHandler_Test()
        {
            string countryCode = "ID";
            var result = await _handler.Handle(new GetAllCityByCountryCodeRequest { CountryCode = countryCode },
                CancellationToken.None);

            result.ShouldBeOfType<List<GetAllCityByCountryCodeResponse>>();
            result.Count.ShouldBe(6);
        }
    }
}