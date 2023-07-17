using eShop.Domain.Entities;

namespace eShop.Application.Abstractions;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
}
