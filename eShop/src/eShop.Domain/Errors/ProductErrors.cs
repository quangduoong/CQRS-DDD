using eShop.Domain.Shared;

namespace eShop.Domain.Errors;

public static class ProductErrors
{
    public static Error NotFound(Guid productId) => 
        new("Product.NotFound", $"Product {productId} not found.");
}

