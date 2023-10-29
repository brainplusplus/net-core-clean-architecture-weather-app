using System;
using MediatR;

namespace WeatherApp.Application.Features.WeatherFeatures.GetWeatherByLocationQuery
{

// public sealed record GetAllCityByCountryCodeRequest : IRequest<List<GetAllCityByCountryCodeResponse>>;

    public class GetWeatherByLocationRequest : IRequest<GetWeatherByLocationResponse>
    {
        public string Q { get; set; }
    }
}