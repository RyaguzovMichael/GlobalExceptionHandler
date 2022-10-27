using GlobalExceptionHandler.Interfaces;

namespace GlobalExceptionHandler;

public sealed class ExceptionHandlerOptions
{
    public Dictionary<Type, IExceptionHandler> Handlers { get; }

    public ExceptionHandlerOptions()
    {
        Handlers = new Dictionary<Type, IExceptionHandler>();
    }
    
    public void Add<T>(IExceptionHandler handler) where T : Exception
    {
        Handlers.Add(typeof(T), handler);
    }
}