using PartsTracker.Shared.Domain;

namespace PartsTracker.Shared.Application.Exceptions;

public sealed class PartsTrackerException : Exception
{
    public PartsTrackerException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
