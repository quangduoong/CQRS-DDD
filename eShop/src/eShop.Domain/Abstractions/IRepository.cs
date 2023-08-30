
using eShop.Domain.Primitives;

namespace eShop.Domain.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity);
}

