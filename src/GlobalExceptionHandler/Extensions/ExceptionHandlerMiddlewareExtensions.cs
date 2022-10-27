using Microsoft.AspNetCore.Builder;

namespace GlobalExceptionHandler.Extensions;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ExceptionHandler>();  
    }  
}