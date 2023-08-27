using eShop.Domain.Abstractions;
using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Infrastructure.Repositories;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public override async Task<Product?> GetByIdAsync(Guid id)
    {
        return await DbContext.Products
            .Where(product => product.Id == id)
            .Include(product => product.PriceCurrency)
            .FirstOrDefaultAsync()
            ?? null;
    }
}
