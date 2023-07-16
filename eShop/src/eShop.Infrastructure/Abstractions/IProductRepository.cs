using eShop.Domain.Entities;

namespace eShop.Infrastructure.Abstractions;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
}
