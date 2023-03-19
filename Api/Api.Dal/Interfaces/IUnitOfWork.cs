using Domain;
using Domain.Interfaces;

namespace Dal.Interfaces;

public interface IUnitOfWork
{
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;
    public Task SaveChanges();
}