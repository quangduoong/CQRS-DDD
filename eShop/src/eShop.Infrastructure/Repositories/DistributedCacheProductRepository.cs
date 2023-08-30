using eShop.Domain.Abstractions;
using eShop.Domain.Entities;
using eShop.Infrastructure.Database;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace eShop.Infrastructure.Repositories;

public class DistributedCacheProductRepository : IProductRepository
{
    private readonly IProductRepository _decorator;
    private readonly IDistributedCache _distributedCache;
    private readonly AppDbContext _dbContext;

    public DistributedCacheProductRepository(
        IProductRepository decorator,
        IDistributedCache distributedCache,
        AppDbContext dbContext)
    {
        _decorator = decorator;
        _distributedCache = distributedCache;
        _dbContext = dbContext;
    }

    public async Task AddAsync(Product product) => await _decorator.AddAsync(product);

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        string cacheKey = $"product-{id}";
        string? cachedProduct = await _distributedCache.GetStringAsync(cacheKey);

        Product? foundProduct;

        // In case there is no cached product found.
        if (string.IsNullOrEmpty(cachedProduct))
        {
            foundProduct = await _decorator.GetByIdAsync(id);

            if (foundProduct is null)
                return null;

            await _distributedCache
                .SetStringAsync(
                    cacheKey,
                    JsonConvert.SerializeObject(foundProduct));

            return foundProduct;
        }

        foundProduct = JsonConvert.DeserializeObject<Product>(cachedProduct);

        if (foundProduct is not null)
            _dbContext.Products.Attach(foundProduct);

        return foundProduct;
    }
}
