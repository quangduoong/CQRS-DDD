using AutoMapper;
using eShop.Application.Products.Commands;
using eShop.Application.Products.Responses;
using eShop.Domain.Abstractions;
using eShop.Domain.Entities;
using eShop.Domain.Shared;
using MediatR;
using System.Data;

namespace eShop.Application.Products.Handlers;

internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = _mapper.Map<Product>(request.Product);
        await _repository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(_mapper.Map<CreateProductResponse>(product));
    }
}
