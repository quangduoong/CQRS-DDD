using eShop.Domain.Abstractions;
using eShop.Domain.Shared;
using MediatR;
using System.Transactions;

namespace eShop.Application.Behaviors;

public class DbTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IUnitOfWork _unitOfWork;

    public DbTransactionBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!typeof(TRequest).Name.EndsWith("Command"))
            return await next();

        using var scope = new TransactionScope();
        try
        {
            var response = await next();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            throw new Exception();

            scope.Complete();

            return response;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            scope.Dispose();
        }
    }
}

