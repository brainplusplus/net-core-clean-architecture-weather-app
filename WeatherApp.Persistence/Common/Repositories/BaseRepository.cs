using WeatherApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Application.Common.Repositories;
using WeatherApp.Domain.Common.Entities;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;

namespace WeatherApp.Persistence.Repositories
{

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext Context;

        public BaseRepository(DataContext context)
        {
            Context = context;
        }

        public async Task<T> Add(T entity)
        {
            await Context.AddAsync(entity);
            return entity;
        }

        public async Task Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public async Task<bool> Exists(Guid id, CancellationToken cancellationToken)
        {
            var entity = await Get(id, cancellationToken);
            return entity.Id != Guid.Empty;
        }

        public async Task<T> Get(Guid id, CancellationToken cancellationToken)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await Context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}