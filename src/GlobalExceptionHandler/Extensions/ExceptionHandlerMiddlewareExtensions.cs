using System.Reflection;
using GlobalExceptionHandler.Exceptions;
using GlobalExceptionHandler.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalExceptionHandler.Extensions;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ExceptionHandlerMiddleware>();  
    }
    
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        var options = new ExceptionHandlerOptions();
        
        AddDefaultHandlers(options);
        
        services.AddSingleton(options);
        return services;
    }
    
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services, Action<ExceptionHandlerOptions> configure)
    {

        var options = new ExceptionHandlerOptions();
        
        AddDefaultHandlers(options);
        
        configure(options);
        
        
        services.AddSingleton(options);
        return services;
    }
    
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var options = new ExceptionHandlerOptions();
        
        AddDefaultHandlers(options);
        
        HandlersAssemblyRegister.AddExceptionHandlersClasses(assemblies, options);
        
        services.AddSingleton(options);
        return services;
    }
    

    private static void AddDefaultHandlers(ExceptionHandlerOptions options)
    {
        options.Add(typeof(InternalServiceException), new InternalServiceExceptionHandler());
        options.Add(typeof(UnauthorizedAccessException), new UnauthorizedAccessExceptionHandler());
        options.Add(typeof(InvalidCastException), new InvalidCastExceptionHandler());
    }
}