using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery;
using WeatherApp.Application.Features.WeatherFeatures.GetWeatherByLocationQuery;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Domain.ResponseDtos.WeatherRestClientResponse;

namespace WeatherApp.WebAPI.Controllers
{

    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("query/{countryCode}/{cityName}")]
        public async Task<ActionResult<GetWeatherByLocationResponse>> GetByCountryCodeAndCityName(string countryCode,
            string cityName, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetWeatherByLocationRequest { Q = $"{cityName},{countryCode}" },
                cancellationToken);
            return Ok(response);
        }
    }
}