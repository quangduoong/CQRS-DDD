namespace eShop.Domain.Shared;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public TValue? Value
    {
        get => !IsSuccess
            ? throw new InvalidOperationException("Cannot access value of a failed result.")
            : _value!;
    }

    public Result(bool isSuccess, Error error) : base(isSuccess, error) { }

    public Result(bool isSuccess, Error error, TValue value) : base(isSuccess, error)
    {
        _value = value;
    }
}