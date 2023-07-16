using eShop.Infrastructure.Products.Responses;
using eShop.Domain.Entities;
using MediatR;

namespace eShop.Infrastructure.Products.Queries;

public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductResponse>;
