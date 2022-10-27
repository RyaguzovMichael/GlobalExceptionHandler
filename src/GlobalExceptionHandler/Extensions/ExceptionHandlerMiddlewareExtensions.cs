using System.Reflection;
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
        GetOrInstantiateExceptionHandlerOptions(services);
        return services;
    }
    
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services, Action<ExceptionHandlerOptions> configure)
    {
        var options = GetOrInstantiateExceptionHandlerOptions(services);
        configure(options);
        return services;
    }
    
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var options = GetOrInstantiateExceptionHandlerOptions(services);
        HandlersAssemblyRegister.AddExceptionHandlersClasses(assemblies, options);
        return services;
    }

    private static ExceptionHandlerOptions GetOrInstantiateExceptionHandlerOptions(IServiceCollection services)
    {
        var options = services.BuildServiceProvider().GetService<ExceptionHandlerOptions>();
        if (options != null) return options;
        options = new ExceptionHandlerOptions();
        AddDefaultHandlers(options);
        services.AddSingleton(options);
        return options;
    }

    private static void AddDefaultHandlers(ExceptionHandlerOptions options)
    {
        options.Add(typeof(UnauthorizedAccessException), new UnauthorizedAccessExceptionHandler());
        options.Add(typeof(InvalidCastException), new InvalidCastExceptionHandler());
    }
}