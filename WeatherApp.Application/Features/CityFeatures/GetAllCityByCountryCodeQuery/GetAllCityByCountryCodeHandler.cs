using AutoMapper;
using WeatherApp.Application.Repositories;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace WeatherApp.Application.Features.CityFeatures.GetAllCityByCountryCodeQuery
{

    public sealed class
        GetAllCityByCountryCodeHandler : IRequestHandler<GetAllCityByCountryCodeRequest,
            List<GetAllCityByCountryCodeResponse>>
    {
        private readonly ICityRepository _CityRepository;
        private readonly IMapper _mapper;

        public GetAllCityByCountryCodeHandler(ICityRepository CityRepository, IMapper mapper)
        {
            _CityRepository = CityRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllCityByCountryCodeResponse>> Handle(GetAllCityByCountryCodeRequest request,
            CancellationToken cancellationToken)
        {
            var Citys = await _CityRepository.GetAllByCountryCode(request.CountryCode, cancellationToken);
            return _mapper.Map<List<GetAllCityByCountryCodeResponse>>(Citys);
        }
    }
}