namespace eShop.Domain.Shared;

public interface IValidationResult
{
    public IEnumerable<Error> Errors { get; }

    public static readonly Error ValidationError
        = new("ValidationError",
            "A validation problem occurred.");
}
