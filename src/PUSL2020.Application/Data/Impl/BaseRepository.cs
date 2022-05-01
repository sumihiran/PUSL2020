using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PUSL2020.Application.Data.Impl;

public abstract class BaseRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
    where TEntity : class where TKey : new()
{
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(IApplicationDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> FindAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return entities;
    }

    public ValueTask<TEntity?> FindByIdAsync(TKey id)
    {
        return _dbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;;
    }

    public Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var results = await _dbSet.Where(predicate).ToListAsync();
        return results;
    }
    
    public Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate).LongCountAsync();
    }
}