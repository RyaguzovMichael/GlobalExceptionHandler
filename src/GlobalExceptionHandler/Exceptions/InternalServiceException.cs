using System.Net;

namespace GlobalExceptionHandler.Exceptions;

public class InternalServiceException : ApplicationException
{
    public int StatusCode { get; }
    
    public InternalServiceException()
    {
        StatusCode = (int)HttpStatusCode.InternalServerError;
    }
    
    public InternalServiceException(string message) : base(message)
    {
        StatusCode = (int)HttpStatusCode.InternalServerError;
    }

    public InternalServiceException(int httpStatusCode, string message) : base(message)
    {
        StatusCode = httpStatusCode;
    }
}