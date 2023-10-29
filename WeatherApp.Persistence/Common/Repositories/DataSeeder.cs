using System;
using WeatherApp.Application.Common.Seedings;
using WeatherApp.Persistence.Context;

namespace WeatherApp.Persistence.Repositories
{

    public abstract class DataSeeder : IDataSeeder
    {
        protected readonly DataContext Context;

        public DataSeeder(DataContext context)
        {
            this.Context = context;
        }

        public void Seed()
        {
            throw new NotImplementedException();
        }
    }
}