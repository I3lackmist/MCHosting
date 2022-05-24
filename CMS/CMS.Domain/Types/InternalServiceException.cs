namespace CMS.Domain.Types;

public class InternalServiceException : Exception {
    public InternalServiceException() {}
    public InternalServiceException(string message) : base(message) {}

    public InternalServiceException(string message, Exception inner) : base(message, inner) {}
}