using eShop.Domain.Abstractions;
using eShop.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace eShop.Infrastructure.Repositories;

public class MemoryCacheProductRepository : IProductRepository
{
    private readonly IProductRepository _decorator;
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheProductRepository(IProductRepository decorator, IMemoryCache memoryCache)
    {
        _decorator = decorator;
        _memoryCache = memoryCache;
    }

    public Task AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        string cacheKey = $"product-{id}";

        return await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            return await _decorator.GetByIdAsync(id);
        });
    }

    public async Task SaveChangesAsync() => await _decorator.SaveChangesAsync();
}

