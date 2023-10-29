using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Common.Repositories;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Repositories
{

    public interface ICountryRepository : IBaseRepository<Country>
    {
        Task<Country> GetByCode(string code, CancellationToken cancellationToken);
    }
}