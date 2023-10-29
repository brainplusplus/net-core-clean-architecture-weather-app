using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery;

namespace WeatherApp.WebAPI.Controllers
{

    [ApiController]
    [Route("city")]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{countryCode}")]
        public async Task<ActionResult<List<GetAllCityByCountryCodeResponse>>> GetAllByCountryCode(string countryCode,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllCityByCountryCodeRequest { CountryCode = countryCode },
                cancellationToken);
            return Ok(response);
        }
    }
}