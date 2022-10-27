using Microsoft.AspNetCore.Http;

namespace GlobalExceptionHandler.Interfaces;

public interface IExceptionHandler
{
    Task HandleAsync(Exception exception, HttpContext context);
}