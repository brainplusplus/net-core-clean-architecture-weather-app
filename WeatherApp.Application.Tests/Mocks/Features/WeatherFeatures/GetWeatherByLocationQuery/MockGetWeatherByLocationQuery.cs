using AutoMapper;
using WeatherApp.Application.Features.WeatherFeatures.GetWeatherByLocationQuery;
using WeatherApp.Application.Tests.Mocks.RestClients;

namespace WeatherApp.Application.Tests.Mocks.Features.WeatherFeatures.GetWeatherByLocationQuery
{
    public class MockGetWeatherByLocationQuery
    {
        public static IMapper GetWeatherByLocationMapper()
        {
            var mapperConfig = new MapperConfiguration(c => { c.AddProfile<GetWeatherByLocationMapper>(); });

            IMapper mapper = mapperConfig.CreateMapper();
            return mapper;
        }
        
        public static GetWeatherByLocationHandler GetWeatherByLocationHandler()
        {
            var handler = new GetWeatherByLocationHandler(MockWeatherRestClient.GetMockWeatherRestClient().Object,
                GetWeatherByLocationMapper());
            return handler;
        }
    }
}