using System;

namespace WeatherApp.Application.Common.Exceptions
{

    public class DynamicException : Exception
    {
        private int statusCode;
        
        public DynamicException(string message) : base(message)
        {
        }
        
        public DynamicException(int _statusCode, string message) : base(message)
        {
            statusCode = _statusCode;
        }

        public int GetStatusCode()
        {
            return statusCode;
        }
    }
}