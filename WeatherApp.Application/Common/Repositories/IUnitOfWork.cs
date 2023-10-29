using System.Threading;
using System.Threading.Tasks;

namespace WeatherApp.Application.Common.Repositories
{

    public interface IUnitOfWork
    {
        Task Save(CancellationToken cancellationToken);
    }
}