using eShop.Infrastructure.Products.Requests;
using eShop.Infrastructure.Products.Responses;
using MediatR;

namespace eShop.Infrastructure.Products.Commands;

public record CreateProductCommand(CreateProductRequest Product) : IRequest<ProductResponse>
{
}
