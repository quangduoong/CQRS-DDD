using eShop.Domain.Entities;
using eShop.Domain.Primitives;

namespace eShop.Domain.Abstractions;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByIdAsync(Guid id);
}
