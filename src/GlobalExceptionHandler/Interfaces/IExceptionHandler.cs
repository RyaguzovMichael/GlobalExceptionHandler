using Microsoft.AspNetCore.Http;

namespace GlobalExceptionHandler.Interfaces;

public interface IExceptionHandler
{
    public Type ExceptionType { get; }
    
    Task HandleAsync(Exception exception, HttpContext context);
}