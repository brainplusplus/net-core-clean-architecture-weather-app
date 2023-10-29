using System.Collections.Generic;
using WeatherApp.Domain.Entities;
using WeatherApp.Persistence.Context;
using WeatherApp.Persistence.Repositories;
using System.Linq;

namespace WeatherApp.Persistence.Seedings
{

    public class CountryDataSeeder : DataSeeder
    {
        public CountryDataSeeder(DataContext Context) : base(Context)
        {
        }

        public void Seed()
        {
            if (!Context.Countries.Any())
            {
                var countries = new List<Country>()
                {
                    new Country()
                    {
                        Code = "AU",
                        Name = "Australia",
                    },
                    new Country()
                    {
                        Code = "ID",
                        Name = "Indonesia",
                    }
                };

                Context.Countries.AddRange(countries);
                Context.SaveChanges();
            }
        }
    }
}