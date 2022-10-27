using System.Net;
using GlobalExceptionHandler.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GlobalExceptionHandler.Handlers;

public class InvalidCastExceptionHandler : IExceptionHandler
{
    public async Task HandleAsync(Exception exception, HttpContext context)
    {
        if (exception is InvalidCastException invalidCastException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var data= "Invalid type cast exception: " + invalidCastException.Message;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}