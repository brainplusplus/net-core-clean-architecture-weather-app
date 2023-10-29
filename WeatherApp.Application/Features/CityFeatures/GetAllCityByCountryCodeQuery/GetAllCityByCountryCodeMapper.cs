using AutoMapper;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery
{

    public sealed class GetAllCityByCountryCodeMapper : Profile
    {
        public GetAllCityByCountryCodeMapper()
        {
            CreateMap<City, GetAllCityByCountryCodeResponse>()
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.Country.Code))
                .ReverseMap();

        }
    }
}