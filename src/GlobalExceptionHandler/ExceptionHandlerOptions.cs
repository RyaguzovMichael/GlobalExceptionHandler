using GlobalExceptionHandler.Interfaces;

namespace GlobalExceptionHandler;

public sealed class ExceptionHandlerOptions
{
    internal Dictionary<Type, IExceptionHandler> Handlers { get; }

    public ExceptionHandlerOptions()
    {
        Handlers = new Dictionary<Type, IExceptionHandler>();
    }
    
    public void Add(Type exceptionType, IExceptionHandler handler)
    {
        Handlers.Add(exceptionType, handler);
    }
}