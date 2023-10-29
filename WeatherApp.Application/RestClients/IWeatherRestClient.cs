using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Domain.ResponseDtos.WeatherRestClientResponse;

namespace WeatherApp.Application.RestClients
{

    public interface IWeatherRestClient
    {
        Task<WeatherRestClientResponseDto> GetByQuery(string query, CancellationToken cancellationToken);
    }
}