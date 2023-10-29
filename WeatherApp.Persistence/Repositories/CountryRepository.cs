using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Repositories;
using WeatherApp.Domain.Entities;
using WeatherApp.Persistence.Context;
using System.Linq;

namespace WeatherApp.Persistence.Repositories
{

    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext context) : base(context)
        {
        }

        public Task<Country> GetByCode(string code, CancellationToken cancellationToken)
        {
            return Context.Countries.FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
        }
    }
}