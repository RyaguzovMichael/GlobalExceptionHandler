using System.Reflection;
using GlobalExceptionHandler.Interfaces;

namespace GlobalExceptionHandler;

internal static class HandlersAssemblyRegister
{
    internal static void AddExceptionHandlersClasses(IEnumerable<Assembly> assembliesToScan, ExceptionHandlerOptions options)
    {
        var concretions = assembliesToScan.SelectMany(a => a.DefinedTypes)
                                          .Where(t => t.GetInterfaces().Contains(typeof(IExceptionHandler)))
                                          .Where(type => type.IsConcrete()).Cast<Type>()
                                          .ToList();

        foreach (var handler in concretions.Select(Activator.CreateInstance))
        {
            if (handler is not IExceptionHandler exceptionHandler) continue;
            if (exceptionHandler.ExceptionType.IsSubclassOf(typeof(Exception)))
            {
                options.Add(exceptionHandler.ExceptionType, exceptionHandler);
            }
            else
            {
                throw new InvalidDataException($"The \"{exceptionHandler.ExceptionType.FullName}\" is not inherited \"Exception\"");
            }
        }
    }

    private static bool IsConcrete(this Type type)
    {
        return !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
    }
}