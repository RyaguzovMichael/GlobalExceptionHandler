using System.Net;
using GlobalExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GlobalExceptionHandler;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;    
    
    public ExceptionHandler(RequestDelegate next)    
    {    
        _next = next;    
    }    
    
    public async Task Invoke(HttpContext context)    
    {    
        try    
        {    
            await _next.Invoke(context);    
        }    
        catch (Exception ex)    
        {    
            await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);         
        }    
    }
    
    private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
    {
        object? data;
        switch (exception)
        {
            case InternalServiceException internalServiceException:
                context.Response.StatusCode = internalServiceException.StatusCode;
                data = "Internal service exception: " + internalServiceException.Message;
                break;
            case InvalidCastException invalidCastException:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                data = "Invalid type cast exception: " + invalidCastException.Message;
                break;
            case UnauthorizedAccessException unauthorizedAccessException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                data = "Authorize exception: " + unauthorizedAccessException.Message;
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                data = exception.Message;
                break;
        }
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonConvert.SerializeObject(data));
    }
}