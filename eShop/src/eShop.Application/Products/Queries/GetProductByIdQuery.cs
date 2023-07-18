using eShop.Application.Products.Responses;
using MediatR;

namespace eShop.Application.Products.Queries;

public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductResponse>;
