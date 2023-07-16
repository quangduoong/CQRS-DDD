using AutoMapper;
using eShop.Infrastructure.Products.Commands;
using eShop.Infrastructure.Products.Responses;
using eShop.Domain.Entities;
using eShop.Infrastructure.Abstractions;
using MediatR;

namespace eShop.Infrastructure.Products.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product newProduct = _mapper.Map<Product>(request.Product);
        await _repository.AddAsync(newProduct);
        return _mapper.Map<ProductResponse>(newProduct);
    }
}
