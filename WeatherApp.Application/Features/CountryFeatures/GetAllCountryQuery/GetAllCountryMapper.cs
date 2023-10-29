using AutoMapper;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery
{

    public sealed class GetAllCountryMapper : Profile
    {
        public GetAllCountryMapper()
        {
            CreateMap<Country, GetAllCountryResponse>();
        }
    }
}