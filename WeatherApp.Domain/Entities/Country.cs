using WeatherApp.Domain.Common.Entities;

namespace WeatherApp.Domain.Entities
{

    public sealed class Country : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}