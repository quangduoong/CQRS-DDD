using Dapper;
using eShop.Domain.Abstractions;
using eShop.Domain.Entities;
using eShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace eShop.Infrastructure.Repositories;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly IDbConnection _dbConnection;

    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbConnection = DbContext.Connection;
    }

    public override async Task AddAsync(Product product)
    {
        _dbConnection.Open();
        using var transaction = _dbConnection.BeginTransaction();

        try
        {
            DbContext.Database.UseTransaction(transaction as DbTransaction);

            string sqlQuery = @"
                    INSERT INTO products (id, name, sku, price_amount, price_currency_id)
                    VALUES (@Id, @Name, @Sku, @PriceAmount, @PriceCurrencyId);
                ";

            await _dbConnection.ExecuteAsync(
                sqlQuery,
                new
                {
                    product.Id,
                    product.Name,
                    product.Sku,
                    product.PriceAmount,
                    product.PriceCurrencyId,
                },
                transaction);

            DbContext.Products.Attach(product);
            await SaveChangesAsync();

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
        finally
        {
            transaction?.Dispose();
            _dbConnection.Close();
        }
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await DbContext.Products
            .Where(product => product.Id == id)
            .Include(product => product.PriceCurrency)
            .FirstOrDefaultAsync()
            ?? null;
    }
}
