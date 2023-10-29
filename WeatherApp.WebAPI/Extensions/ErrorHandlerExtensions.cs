using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using WeatherApp.Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Drawing;

namespace WeatherApp.WebAPI.Extensions
{

    public static class ErrorHandlerExtensions
    {
        private static int GetErrorStatusCode(Exception _exception)
        {
            Type exceptionType = _exception.GetType();
            if (exceptionType == typeof(BadRequestException))
            {
                return (int)HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(BadRequestException)) 
            {
                return (int)HttpStatusCode.ServiceUnavailable;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                return (int)HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(DynamicException))
            {
                return ((DynamicException)_exception).GetStatusCode();
            }
            else
            {
                return (int)HttpStatusCode.InternalServerError;
            }
        }

        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null) return;

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = GetErrorStatusCode(contextFeature.Error);
                    var errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = contextFeature.Error.GetBaseException().Message
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                });
            });
        }
    }
}