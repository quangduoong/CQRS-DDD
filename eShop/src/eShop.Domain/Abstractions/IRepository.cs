
using eShop.Domain.Primitives;

namespace eShop.Domain.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task AddAsync(TEntity entity);
    Task SaveChangesAsync();
}

