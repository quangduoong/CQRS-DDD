using eShop.Domain.Products.Requests;
using eShop.Domain.Products.Responses;
using MediatR;

namespace eShop.Domain.Products.Commands;

public record CreateProductCommand(CreateProductRequest Product) : IRequest<ProductResponse>
{
}
