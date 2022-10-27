using GlobalExceptionHandler.Extensions;
using GlobalExceptionHandlerExample.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddExceptionHandlers(configure =>
{
    configure.Add(typeof(InvalidOperationException), new InvalidOperationExceptionHandler());
});

var app = builder.Build();

app.UseGlobalExceptionHandler();
app.MapGet("/", () =>
{
    throw new InvalidOperationException("This is my exception");
});

app.Run();