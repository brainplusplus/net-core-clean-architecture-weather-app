using WeatherApp.Domain.Common.Entities;

namespace WeatherApp.Domain.Entities
{
    public sealed class City : BaseEntity
    {
        public Country Country { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}