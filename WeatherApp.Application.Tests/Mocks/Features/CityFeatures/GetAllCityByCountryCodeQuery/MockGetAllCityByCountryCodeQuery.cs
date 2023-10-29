using AutoMapper;
using WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery;
using WeatherApp.Application.Tests.Mocks.Repositories;

namespace WeatherApp.Application.Tests.Mocks.Features.CityFeatures.GetAllCityByCountryCodeQuery
{

    public class MockGetAllCityByCountryCodeQuery
    {
        public static IMapper GetAllCityByCountryCodeMapper()
        {
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile<GetAllCityByCountryCodeMapper>(); });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }

        public static GetAllCityByCountryCodeHandler GetAllCityByCountryCodeHandler()
        {
            var handler = new GetAllCityByCountryCodeHandler(MockCityRepository.GetMockCityRepository().Object,
                GetAllCityByCountryCodeMapper());
            return handler;
        }
    }
}