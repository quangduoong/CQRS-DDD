using eShop.Domain.Entities;
using MediatR;

namespace eShop.Infrastructure.Products.Requests;

public record CreateProductRequest
{
    public string? Name { get; init; }
    public int? Sku { get; init; }
    public double? PriceAmount { get; init; }
    public PriceCurrency PriceCurrency { get; init; }
}
