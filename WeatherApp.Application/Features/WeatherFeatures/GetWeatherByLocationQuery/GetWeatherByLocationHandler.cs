using System;
using AutoMapper;
using WeatherApp.Application.Repositories;
using MediatR;
using WeatherApp.Application.RestClients;
using System.Threading.Tasks;
using System.Threading;
using WeatherApp.Domain.ResponseDtos.WeatherRestClientResponse;

namespace WeatherApp.Application.Features.WeatherFeatures.GetWeatherByLocationQuery
{

    public sealed class
        GetWeatherByLocationHandler : IRequestHandler<GetWeatherByLocationRequest, GetWeatherByLocationResponse>
    {
        private readonly IWeatherRestClient _weatherRestClient;
        private readonly IMapper _mapper;

        public GetWeatherByLocationHandler(IWeatherRestClient weatherRestClient, IMapper mapper)
        {
            _weatherRestClient = weatherRestClient;
            _mapper = mapper;
        }

        public async Task<GetWeatherByLocationResponse> Handle(GetWeatherByLocationRequest request,
            CancellationToken cancellationToken)
        {
            var weather = await _weatherRestClient.GetByQuery(request.Q, cancellationToken);
            return _mapper.Map<GetWeatherByLocationResponse>(weather);
        }
    }
}