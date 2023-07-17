using eShop.Application.Products.Requests;
using eShop.Application.Products.Responses;
using MediatR;

namespace eShop.Application.Products.Commands;

public record CreateProductCommand(CreateProductRequest Product) : IRequest<ProductResponse>
{
}
