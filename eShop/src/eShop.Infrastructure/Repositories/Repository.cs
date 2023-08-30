using eShop.Domain.Abstractions;
using eShop.Domain.Primitives;
using eShop.Infrastructure.Database;

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

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.RollbackToSavepoint(transactionName);
        }
    }
}

