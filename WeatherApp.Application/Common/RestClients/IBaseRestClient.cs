using System;

namespace WeatherApp.Application.Common.RestClients
{
    public interface IBaseRestClient<T>
    {
        void SetupHeaders();
    }
}