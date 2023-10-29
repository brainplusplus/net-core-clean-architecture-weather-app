using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Common.Repositories;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Repositories
{

    public interface ICityRepository : IBaseRepository<City>
    {
        Task<List<City>> GetAllByCountryCode(string countryCode, CancellationToken cancellationToken);
    }
}