using AutoMapper;
using Moq;
using WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery;
using WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery;
using WeatherApp.Application.Tests.Mocks.Repositories;

namespace WeatherApp.Application.Tests.Mocks.Features.CountryFeatures.GetAllCountryQuery
{

    public class MockGetAllCountryQuery
    {
        public static IMapper GetAllCountryMapper()
        {
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile<GetAllCountryMapper>(); });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }

        public static GetAllCountryHandler GetAllCountryHandler()
        {
            var handler = new GetAllCountryHandler(MockCountryRepository.GetMockCountryRepository().Object,
                GetAllCountryMapper());
            return handler;
        }
    }
}