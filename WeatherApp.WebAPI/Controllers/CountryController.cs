using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherApp.WebAPI.Controllers
{

    [ApiController]
    [Route("country")]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllCountryResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllCountryRequest(), cancellationToken);
            return Ok(response);
        }
    }
}