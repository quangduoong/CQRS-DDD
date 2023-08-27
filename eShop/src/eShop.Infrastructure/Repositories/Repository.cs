using eShop.Domain.Abstractions;
using eShop.Domain.Primitives;
using System.Data.Entity;

namespace eShop.Infrastructure.Repositories;

internal abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected readonly AppDbContext DbContext;

    protected Repository(AppDbContext dbContext) => DbContext = dbContext;

    public virtual async Task AddAsync(TEntity entity)
    {
        await DbContext.Set<TEntity>().AddAsync(entity);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<TEntity>()
            .FirstOrDefaultAsync(entity => entity.Id == id)
            ?? null;
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}

