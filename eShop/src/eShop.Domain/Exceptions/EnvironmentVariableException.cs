namespace eShop.Domain.Exceptions;

public class EnvironmentVariableException : Exception
{
    public EnvironmentVariableException(string message) : base(message) { }

    public EnvironmentVariableException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    public static EnvironmentVariableException NotAvailable() => new("Environment not available.");
}

