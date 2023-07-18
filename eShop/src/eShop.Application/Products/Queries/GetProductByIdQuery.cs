using eShop.Domain.Products.Responses;
using MediatR;

namespace eShop.Domain.Products.Queries;

public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductResponse>;
