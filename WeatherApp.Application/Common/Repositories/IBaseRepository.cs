using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Domain.Common.Entities;

namespace WeatherApp.Application.Common.Repositories
{

    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Add(T entity);

        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> Exists(Guid id, CancellationToken cancellationToken);
        Task<T> Get(Guid id, CancellationToken cancellationToken);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
    }
}