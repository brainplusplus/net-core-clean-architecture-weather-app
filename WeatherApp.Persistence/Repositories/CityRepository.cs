using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Repositories;
using WeatherApp.Domain.Entities;
using WeatherApp.Persistence.Context;
using System.Linq;

namespace WeatherApp.Persistence.Repositories
{

    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(DataContext context) : base(context)
        {
        }

        public Task<List<City>> GetAllByCountryCode(string countryCode, CancellationToken cancellationToken)
        {
            return Context.Cities.Include(city => city.Country).Where(x => x.Country.Code == countryCode)
                .ToListAsync(cancellationToken);
        }
    }
}