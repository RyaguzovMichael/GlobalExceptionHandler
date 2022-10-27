using System.Net;
using GlobalExceptionHandler.Interfaces;
using Newtonsoft.Json;

namespace GlobalExceptionHandlerExample.ExceptionHandlers;

public class InvalidOperationExceptionHandler : IExceptionHandler
{
    public Type ExceptionType { get; }
    
    public InvalidOperationExceptionHandler()
    {
        ExceptionType = typeof(InvalidOperationException);
    }
    public async Task HandleAsync(Exception exception, HttpContext context)
    {
        if (exception is InvalidOperationException invalidOperationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var data = "Invalid operation exception: " + invalidOperationException.Message;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}