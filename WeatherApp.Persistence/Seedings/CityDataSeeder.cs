using Microsoft.EntityFrameworkCore;
using WeatherApp.Application.Common.Seedings;
using WeatherApp.Domain.Entities;
using WeatherApp.Persistence.Context;
using WeatherApp.Persistence.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace WeatherApp.Persistence.Seedings
{

    public class CityDataSeeder : DataSeeder
    {
        public CityDataSeeder(DataContext Context) : base(Context)
        {
        }

        public void Seed()
        {
            if (!Context.Cities.Any())
            {
                var countryID = Context.Countries.First(x => x.Code == "ID");
                var citiesID = new List<City>()
                {
                    new City()
                    {
                        Country = countryID,
                        Name = "Bekasi",
                        Lat = -6.2349,
                        Lon = 106.9896
                    },
                    new City()
                    {
                        Country = countryID,
                        Name = "Bogor",
                        Lat = -6.5944,
                        Lon = 106.7892
                    },
                    new City()
                    {
                        Country = countryID,
                        Name = "Depok",
                        Lat = -6.402484,
                        Lon = 106.794243
                    },
                    new City()
                    {
                        Country = countryID,
                        Name = "Jakarta",
                        Lat = -6.2146,
                        Lon = 106.8451
                    },
                    new City()
                    {
                        Country = countryID,
                        Name = "Tangerang",
                        Lat = -6.1781,
                        Lon = 106.63
                    },
                    new City()
                    {
                        Country = countryID,
                        Name = "Tangerang Selatan",
                        Lat = -6.326740,
                        Lon = 106.730042
                    }

                };

                var countryAU = Context.Countries.First(x => x.Code == "AU");
                var citiesAU = new List<City>()
                {
                    new City()
                    {
                        Country = countryAU,
                        Name = "Adelaide",
                        Lat = -34.928497,
                        Lon = 138.600739
                    },
                    new City()
                    {
                        Country = countryAU,
                        Name = "Brisbane",
                        Lat = -27.4679,
                        Lon = 153.0281
                    },
                    new City()
                    {
                        Country = countryAU,
                        Name = "Canberra",
                        Lat = -35.2835,
                        Lon = 149.1281
                    },
                    new City()
                    {
                        Country = countryAU,
                        Name = "Darwin",
                        Lat = -12.4611,
                        Lon = 130.8418
                    },
                    new City()
                    {
                        Country = countryAU,
                        Name = "Perth",
                        Lat = -31.9333,
                        Lon = 115.8333
                    },
                    new City()
                    {
                        Country = countryAU,
                        Name = "Sydney",
                        Lat = -33.8679,
                        Lon = 151.2073
                    }

                };

                Context.Cities.AddRange(citiesAU);
                Context.Cities.AddRange(citiesID);
                Context.SaveChanges();
            }
        }
    }
}