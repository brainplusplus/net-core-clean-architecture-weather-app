using System;

namespace WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery
{
    public sealed class GetAllCountryResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}