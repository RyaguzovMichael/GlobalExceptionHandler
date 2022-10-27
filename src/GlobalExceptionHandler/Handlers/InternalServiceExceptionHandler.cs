using GlobalExceptionHandler.Exceptions;
using GlobalExceptionHandler.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GlobalExceptionHandler.Handlers;

public class InternalServiceExceptionHandler : IExceptionHandler
{
    public async Task HandleAsync(Exception exception, HttpContext context)
    {
        if (exception is InternalServiceException internalServiceException)
        {
            context.Response.StatusCode = internalServiceException.StatusCode;
            var data = "Internal service exception: " + internalServiceException.Message;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}