﻿using eShop.Infrastructure.Abstractions;
using eShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) => _context = context;

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(product => product.Id == id) ?? null;
    }
}
