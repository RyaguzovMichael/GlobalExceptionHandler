using GlobalExceptionHandler.Exceptions;
using GlobalExceptionHandler.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GlobalExceptionHandler.Extensions;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<ExceptionHandlerOptions>();
        var options = services.BuildServiceProvider().GetRequiredService<ExceptionHandlerOptions>();
        
        AddDefaultHandlers(options);
        
        return services;
    }
    
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services, Action<ExceptionHandlerOptions> configure)
    {
        services.TryAddSingleton<ExceptionHandlerOptions>();
        var options = services.BuildServiceProvider().GetRequiredService<ExceptionHandlerOptions>();
        
        AddDefaultHandlers(options);
        
        configure(options);
        
        return services;
    }
    
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ExceptionHandlerMiddleware>();  
    }

    private static void AddDefaultHandlers(ExceptionHandlerOptions options)
    {
        options.Add<InternalServiceException>(new InternalServiceExceptionHandler());
        options.Add<UnauthorizedAccessException>(new UnauthorizedAccessExceptionHandler());
    }
}