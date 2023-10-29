using AutoMapper;
using WeatherApp.Application.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherApp.Application.Features.CountryFeatures.GetAllCountryQuery
{

    public sealed class GetAllCountryHandler : IRequestHandler<GetAllCountryRequest, List<GetAllCountryResponse>>
    {
        private readonly ICountryRepository _CountryRepository;
        private readonly IMapper _mapper;

        public GetAllCountryHandler(ICountryRepository CountryRepository, IMapper mapper)
        {
            _CountryRepository = CountryRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllCountryResponse>> Handle(GetAllCountryRequest request,
            CancellationToken cancellationToken)
        {
            var Countrys = await _CountryRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllCountryResponse>>(Countrys);
        }
    }
}