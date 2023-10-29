using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Application.RestClients;
using WeatherApp.Domain.ResponseDtos.WeatherRestClientResponse;
using System.Linq;
using WeatherApp.Application.Common.Exceptions;

namespace WeatherApp.Application.Tests.Mocks.RestClients
{
    public class MockWeatherHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _code;
        
        public MockWeatherHttpMessageHandler(HttpStatusCode code)
        {
            _code = code;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                StatusCode = _code,
            });
        }
    }
    
    public class MockWeatherRestClient
    {
        public static Mock<IWeatherRestClient> GetMockWeatherRestClient()
        {
            List<string> queryList = new List<string>();
            string[] countries = new string[] { "ID", "AU" };
            string[] citiesID = new string[] { "Bekasi", "Bogor", "Depok", "Jakarta", "Tangerang", "Tangerang Selatan" };
            string[] citiesAU = new string[] { "Adelaide", "Brisbane", "Canberra", "Darwin", "Perth", "Sydney" };
            foreach (var city in citiesAU)
            {
                queryList.Add(city);
                queryList.Add(city+",AU");
            }
            foreach (var city in citiesID)
            {
                queryList.Add(city);
                queryList.Add(city+",ID");
            }
            
            var mockRestClient = new Mock<IWeatherRestClient>();
            mockRestClient.Setup(r => r.GetByQuery(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(
                (string query, CancellationToken token) =>
                {
                    if (query.Contains("ERROR"))
                    {
                        WeatherErrorResponseDto data =
                            JsonConvert.DeserializeObject<WeatherErrorResponseDto>(GetJson_500());
                        // return Task.FromResult(data);
                        throw new Exception(data.Message);
                    }
                    else if (query.Contains("QUOTA_LIMIT") || query.Contains("WRONG_API_KEY"))
                    {
                        WeatherErrorResponseDto data =
                            JsonConvert.DeserializeObject<WeatherErrorResponseDto>(GetJson_401());
                        // return Task.FromResult(data);
                        throw new DynamicException(401,data.Message);
                    }
                    else if (queryList.Contains(query))
                    {
                        WeatherRestClientResponseDto data = JsonConvert.DeserializeObject<WeatherRestClientResponseDto>(GetJson_200());
                        return Task.FromResult(data);
                    }
                    else
                    {
                        WeatherErrorResponseDto data =
                            JsonConvert.DeserializeObject<WeatherErrorResponseDto>(GetJson_404());
                        // return Task.FromResult(data);
                        throw new DynamicException(404,data.Message);
                    }
                });
            return mockRestClient;
        }

        public static string GetJson_401()
        {
            return "{\"cod\":401,\"message\":\"Invalid API key. Please see https://openweathermap.org/faq#error401 for more info.\"}";
        }
        
        public static string GetJson_500()
        {
            return "{\"cod\":500,\"message\":\"Error Occurred.\"}";
        }

        public static string GetJson_404()
        {
            return "{\"cod\":\"404\",\"message\":\"city not found\"}";
        }

        public static string GetJson_200()
        {
            return
                "{\"coord\":{\"lon\":151.2073,\"lat\":-33.8679},\"weather\":[{\"id\":500,\"main\":\"Rain\"," +
                "\"description\":\"light rain\",\"icon\":\"10d\"}],\"base\":\"stations\",\"main\":{\"temp\":296.17," +
                "\"feels_like\":295.96,\"temp_min\":293.37,\"temp_max\":302.36,\"pressure\":1015," +
                "\"humidity\":55},\"visibility\":10000,\"wind\":{\"speed\":8.75,\"deg\":30}," +
                "\"rain\":{\"1h\":0.42},\"clouds\":{\"all\":0},\"dt\":1698558458," +
                "\"sys\":{\"type\":2,\"id\":2018875,\"country\":\"AU\",\"sunrise\":1698519506,\"sunset\":1698567555}," +
                "\"timezone\":39600,\"id\":2147714,\"name\":\"Sydney\",\"cod\":200}";
        }
    }
}