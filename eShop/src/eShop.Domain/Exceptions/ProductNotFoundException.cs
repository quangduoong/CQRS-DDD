using eShop.Domain.Entities;

namespace eShop.Domain.Exceptions;

public class ProductNotFoundException : Exception
{
    public Guid RequestedProductId { get; private set; }

    public ProductNotFoundException()
    {
    }

    public ProductNotFoundException(string message)
        : base(message)
    {
    }

    public ProductNotFoundException(Guid id) : this($"Product {id} not found.")
    {
        RequestedProductId = id;
    }

    public ProductNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
