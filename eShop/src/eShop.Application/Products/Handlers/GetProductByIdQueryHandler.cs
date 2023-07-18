using Dapper;
using eShop.Application.Products.Queries;
using eShop.Application.Products.Responses;
using eShop.Domain.Exceptions;
using MediatR;
using System.Data;
using eShop.Application.Abstractions;
using AutoMapper;
using eShop.Domain.Entities;

namespace eShop.Application.Products.Handlers;

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
        Product? product = await _repository.GetByIdAsync(request.ProductId);

        return _mapper.Map<ProductResponse>(product)
            ?? throw new ProductNotFoundException(request.ProductId);
    }
}
