using MediatR;
using System.Collections.Generic;

namespace WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery
{
    public sealed class GetAllCountryRequest : IRequest<List<GetAllCountryResponse>>
    {
    }
}