using System.Net;
using GlobalExceptionHandler.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GlobalExceptionHandler;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;    
    
    public ExceptionHandlerMiddleware(RequestDelegate next)    
    {    
        _next = next;    
    }    
    
    public async Task Invoke(HttpContext context, ExceptionHandlerOptions options)    
    {    
        try    
        {    
            await _next.Invoke(context);    
        }    
        catch (Exception exception)    
        {    
            if (options.Handlers.TryGetValue(exception.GetType(), out IExceptionHandler? handler))
            {
                await handler.HandleAsync(exception, context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                object? data = exception.Message;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
            }
        }    
    }
}