using eShop.Application.Products.Responses;
using eShop.Domain.Entities;
using MediatR;

namespace eShop.Application.Products.Queries;

public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductResponse>;
