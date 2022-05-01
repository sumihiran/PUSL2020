using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PUSL2020.Application.Data;

namespace PUSL2020.Infrastructure.Data.Repositories;

public abstract class BaseRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
    where TEntity : class where TKey : new()
{
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(IApplicationDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> FindAll()
    {
        return _dbSet.AsNoTracking();
    }

    public virtual ValueTask<TEntity?> FindByIdAsync(TKey id)
    {
        return _dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;;
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate).AnyAsync();
    }

    public virtual IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate).AsNoTracking();
    }
    
    public virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate).LongCountAsync();
    }
}