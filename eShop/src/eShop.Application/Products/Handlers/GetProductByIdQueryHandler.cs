using AutoMapper;
using eShop.Application.Products.Queries;
using eShop.Application.Products.Responses;
using eShop.Domain.Abstractions;
using eShop.Domain.Entities;
using eShop.Domain.Errors;
using eShop.Domain.Shared;
using MediatR;

namespace eShop.Application.Products.Handlers;

internal sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductQueryResponse>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ProductQueryResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _repository.GetByIdAsync(request.ProductId);

        if (product is null)
            return Result.Failure<ProductQueryResponse>(ProductErrors.NotFound(request.ProductId));

        ProductQueryResponse response = _mapper.Map<ProductQueryResponse>(product);

        return Result.Success(response);
    }
}
