using MediatR;
using System.Collections.Generic;

namespace WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery
{

// public sealed record GetAllCityByCountryCodeRequest : IRequest<List<GetAllCityByCountryCodeResponse>>;

    public class GetAllCityByCountryCodeRequest : IRequest<List<GetAllCityByCountryCodeResponse>>
    {
        public string CountryCode { get; set; }
    }
}