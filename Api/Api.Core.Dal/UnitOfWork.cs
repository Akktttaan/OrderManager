using System.Collections.Concurrent;
using Dal.Interfaces;
using Domain.Interfaces;

namespace Dal;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ConcurrentDictionary<Type, object> _repsDictionary;
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _repsDictionary = new ConcurrentDictionary<Type, object>();
    }

    public IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IBaseEntity
    {
        if (_repsDictionary.TryGetValue(typeof(TEntity), out object repository))
            return (IRepository<TEntity>)repository;
        repository = new Repository<TEntity>(_dbContext);
        _repsDictionary.TryAdd(typeof(TEntity), repository);
        return (IRepository<TEntity>)repository;
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }

    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            _dbContext.Dispose();
        }

        _disposed = true;
    }

    ~UnitOfWork() => Dispose(false);
}