using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.Common.Repositories;
using WeatherApp.Persistence.Context;

namespace WeatherApp.Persistence.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}