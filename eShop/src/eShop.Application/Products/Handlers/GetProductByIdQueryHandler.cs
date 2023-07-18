using AutoMapper;
using eShop.Domain.Abstractions;
using eShop.Domain.Entities;
using eShop.Domain.Exceptions;
using eShop.Domain.Products.Queries;
using eShop.Domain.Products.Responses;
using MediatR;

namespace eShop.Domain.Products.Handlers;

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
