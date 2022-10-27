using System.Net;
using GlobalExceptionHandler.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GlobalExceptionHandler.Handlers;

public class UnauthorizedAccessExceptionHandler : IExceptionHandler
{
    public async Task HandleAsync(Exception exception, HttpContext context)
    {
        if (exception is UnauthorizedAccessException unauthorizedAccessException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var data= "Authorize exception: " + unauthorizedAccessException.Message;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}