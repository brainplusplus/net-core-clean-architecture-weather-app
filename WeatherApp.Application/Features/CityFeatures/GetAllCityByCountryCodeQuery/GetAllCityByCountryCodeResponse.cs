using System;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery
{

    public sealed class GetAllCityByCountryCodeResponse
    {
        public Guid Id { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}