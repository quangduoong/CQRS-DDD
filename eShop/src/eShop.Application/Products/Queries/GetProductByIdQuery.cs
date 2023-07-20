using eShop.Application.Products.Responses;
using eShop.Domain.Shared;
using MediatR;

namespace eShop.Application.Products.Queries;

public record GetProductByIdQuery(Guid ProductId) : IRequest<Result<ProductQueryResponse>>;
