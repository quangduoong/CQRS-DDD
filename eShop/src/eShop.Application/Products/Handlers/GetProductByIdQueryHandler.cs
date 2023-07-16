using Dapper;
using eShop.Infrastructure.Products.Queries;
using eShop.Infrastructure.Products.Responses;
using eShop.Domain.Exceptions;
using MediatR;
using System.Data;
using eShop.Infrastructure.Abstractions;
using AutoMapper;

namespace eShop.Infrastructure.Products.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.ProductId);

        return _mapper.Map<ProductResponse>(product)
            ?? throw new ProductNotFoundException(request.ProductId);
    }
}
