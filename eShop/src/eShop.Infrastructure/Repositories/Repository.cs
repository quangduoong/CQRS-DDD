using eShop.Domain.Abstractions;
using eShop.Domain.Primitives;
using eShop.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace eShop.Infrastructure.Repositories;

internal abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly AppDbContext DbContext;

    protected Repository(
        AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        using var transaction = DbContext.Database.BeginTransaction();
        const string transactionName = $"BeforeAdding{nameof(TEntity)}";

        try
        {
            transaction.CreateSavepoint(transactionName);

            await DbContext.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.RollbackToSavepoint(transactionName);
        }
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}

