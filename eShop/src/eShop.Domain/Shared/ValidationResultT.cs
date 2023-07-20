namespace eShop.Domain.Shared;

public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public IEnumerable<Error> Errors { get; }

    public ValidationResult(IEnumerable<Error> errors) : base(false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }
}

