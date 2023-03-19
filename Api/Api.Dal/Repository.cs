using System.Linq.Expressions;
using Dal.Interfaces;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
{
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        return _dbSet.Where(expression).AsQueryable();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<TEntity> FirstOrDefaultAsync()
    {
        return await _dbSet.FirstOrDefaultAsync();
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync()
    {
        return GetAll();
    }

    public bool Any(Expression<Func<TEntity, bool>> expression = null) => _dbSet.Any(expression);
}