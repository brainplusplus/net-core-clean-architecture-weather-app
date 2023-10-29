using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Application.Common.RestClients;

namespace WeatherApp.Service.Common.RestClients
{

    public abstract class BaseRestClient<T> : IBaseRestClient<T> where T : class
    {
        private const string MEDIA_TYPE = "application/json";
        protected readonly HttpClient _http;

        public BaseRestClient(string baseAddress)
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri(baseAddress);
        }
        
        public BaseRestClient(HttpClient http)
        {
            _http = http;
        }


        #region Client Helper Methods

        public virtual void SetupHeaders()
        {
            _http.DefaultRequestHeaders.Clear();

            //Define request data format  
            _http.DefaultRequestHeaders.Accept.Add
            (new MediaTypeWithQualityHeaderValue
                (MEDIA_TYPE));
        }

        #endregion
    }
}