using eShop.Application.Products.Requests;
using eShop.Application.Products.Responses;
using eShop.Domain.Shared;
using FluentValidation;
using MediatR;

namespace eShop.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        IEnumerable<Error> failures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validator => validator.Errors)
            .Where(error => error != null)
            .Select(error => new Error(
                error.PropertyName,
                error.ErrorMessage))
            .Distinct()
            .ToList();

        if (failures.Any())
        {
            return CreateValidationResult<TResponse>(failures);
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(IEnumerable<Error> failures)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
            return (new ValidationResult(failures) as TResult)!;

        object validationResult = typeof(ValidationResult<>)
           .GetGenericTypeDefinition()
           .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
           .GetConstructor(new[] { typeof(IEnumerable<Error>) })!
           .Invoke(new[] { failures });

        return (TResult)validationResult;
    }
}
