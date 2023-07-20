namespace eShop.Domain.Shared;

public class Error : IEquatable<Error>
{
    public string Code { get; }

    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    // Instances of Error class
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result is null.");

    public bool Equals(Error? other)
    {
        if (this is null && other is null) return true;
        if (this.Code == other?.Code) return true;

        return false;
    }

    public override bool Equals(object? obj) => Equals(obj as Error);
    public override int GetHashCode() => Code.GetHashCode();

    public static bool operator ==(Error a, Error? b) => a.Equals(b);
    public static bool operator !=(Error a, Error? b) => !a.Equals(b);

}