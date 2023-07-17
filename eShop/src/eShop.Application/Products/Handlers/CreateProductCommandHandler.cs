using AutoMapper;
using eShop.Application.Products.Commands;
using eShop.Application.Products.Responses;
using eShop.Domain.Entities;
using eShop.Application.Abstractions;
using MediatR;

namespace eShop.Application.Products.Handlers;

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
