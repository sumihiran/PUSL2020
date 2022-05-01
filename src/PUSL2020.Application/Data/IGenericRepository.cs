using System.Linq.Expressions;

namespace PUSL2020.Application.Data;

public interface IGenericRepository<TEntity, in TKey> where TEntity : class where TKey : new()
{
    Task<IEnumerable<TEntity>> FindAllAsync();
    ValueTask<TEntity?> FindByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
}