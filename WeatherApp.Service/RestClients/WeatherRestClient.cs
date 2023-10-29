using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Application.RestClients;
using WeatherApp.Domain.Entities;
using WeatherApp.Domain.ResponseDtos.WeatherRestClientResponse;
using WeatherApp.Service.Common.RestClients;
using System.Linq;
using System.Net;
using System.Net.Http;
using WeatherApp.Application.Common.Exceptions;

namespace WeatherApp.Service.RestClients
{

    public class WeatherRestClient : BaseRestClient<Country>, IWeatherRestClient
    {
        protected readonly string ApiKey;

        public WeatherRestClient(string baseAddress, string apiKey) : base(baseAddress)
        {
            ApiKey = apiKey;
        }
        
        public WeatherRestClient(HttpClient http, string apiKey) : base(http)
        {
            ApiKey = apiKey;
        }


        public async Task<WeatherRestClientResponseDto> GetByQuery(string query, CancellationToken cancellationToken)
        {
            try
            {
                SetupHeaders();


                var queryParams = new Dictionary<string, string>
                {
                    ["appid"] = ApiKey,
                    ["units"] = "imperial",
                    ["q"] = query,
                };
                var queryString = string.Join("&",
                    queryParams.Select(kvp =>
                        $"{System.Net.WebUtility.UrlEncode(kvp.Key)}={System.Net.WebUtility.UrlEncode(kvp.Value)}"));

                var response = await _http.GetAsync($"weather?{queryString}", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var returnModel = JsonConvert.DeserializeObject<WeatherRestClientResponseDto>(result);

                    return returnModel;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var returnModelError = JsonConvert.DeserializeObject<WeatherErrorResponseDto>(result);

                    throw new DynamicException(returnModelError.Cod,returnModelError.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown Error" + ex.Message);
            }
        }
    }
}