using AutoMapper;
using WeatherApp.Domain.Entities;
using WeatherApp.Domain.ResponseDtos.WeatherResponse;
using WeatherApp.Domain.ResponseDtos.WeatherRestClientResponse;

namespace WeatherApp.Application.Features.WeatherFeatures.GetWeatherByLocationQuery
{

    public sealed class GetWeatherByLocationMapper : Profile
    {
        public GetWeatherByLocationMapper()
        {
            CreateMap<WeatherRestClientResponseDto, GetWeatherByLocationResponse>()
                .ForPath(dest => dest.Location.Lat, opt => opt.MapFrom(src => src.Coord.Lat))
                .ForPath(dest => dest.Location.Lon, opt => opt.MapFrom(src => src.Coord.Lon))
                .ForPath(dest => dest.Location.CityName, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Location.CountryId, opt => opt.MapFrom(src => src.Sys.Country))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Dt))
                .ForMember(dest => dest.Wind, opt => opt.MapFrom(src =>
                    new WeatherApp.Domain.ResponseDtos.WeatherResponse.Wind
                    {
                        Speed = src.Wind.Speed,
                        Gust = src.Wind.Gust,
                        Deg = src.Wind.Deg
                    }))
                .ForMember(dest => dest.SkyConditions, opt => opt.MapFrom(src =>
                    new WeatherApp.Domain.ResponseDtos.WeatherResponse.SkyCondition
                    {
                        Id = src.Weather[0].Id,
                        Main = src.Weather[0].Main,
                        Description = src.Weather[0].Description,
                        Icon = src.Weather[0].Icon,
                    }))
                .ForPath(dest => dest.TemperatureFahrenheit, opt => opt.MapFrom(src => src.Main.Temp))
                .ForPath(dest => dest.RelativeHumidity, opt => opt.MapFrom(src => src.Main.Humidity))
                .ForPath(dest => dest.Pressure, opt => opt.MapFrom(src => src.Main.Pressure))
                .ReverseMap();
        }
    }
}