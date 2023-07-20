namespace eShop.Domain.Shared;

public class Result
{
    public bool IsSuccess { get; }

    public Error Error { get; }

    public Result(bool isSuccess, Error error)
    {
        if ((isSuccess && error != Error.None) ||
            (!isSuccess && error == Error.None))
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Success<TValue>(TValue value) => new(true, Error.None, value);

    public static Result<TValue> Failure<TValue>(Error error)
        => new(false, error);
}