using CatalogService.Domain.Models.Entities.Bases;
using System.Linq.Expressions;

namespace CatalogService.Application.Repositories
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>, new()
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}
