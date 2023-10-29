using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeatherApp.Domain.ResponseDtos.WeatherResponse
{

    public class WeatherResponseDto
    {
        [JsonProperty("location")] public Location Location { get; set; }

        [JsonProperty("time")] public long Time { get; set; }

        [JsonProperty("wind")] public Wind Wind { get; set; }

        [JsonProperty("visibility")] public long Visibility { get; set; }

        [JsonProperty("sky_conditions")] public SkyCondition SkyConditions { get; set; }

        [JsonProperty("temperature_fahrenheit")]
        public double TemperatureFahrenheit { get; set; }

        [JsonProperty("temperature_celcius")] public double TemperatureCelcius => (TemperatureFahrenheit - 32) * 5 / 9;

        [JsonProperty("dew_point")] public double DewPoint { get; set; }

        [JsonProperty("relative_humidity")] public long RelativeHumidity { get; set; }

        [JsonProperty("pressure")] public long Pressure { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("lon")] public double Lon { get; set; }

        [JsonProperty("lat")] public double Lat { get; set; }

        [JsonProperty("country_id")] public string CountryId { get; set; }

        [JsonProperty("city_name")] public string CityName { get; set; }
    }

    public partial class SkyCondition
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("main")] public string Main { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("icon")] public string Icon { get; set; }
    }

    public partial class Wind
    {
        [JsonProperty("speed")] public double Speed { get; set; }

        [JsonProperty("deg")] public long Deg { get; set; }

        [JsonProperty("gust")] public double Gust { get; set; }
    }
}