using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        void Add(TEntity entity);
        TEntity? GetById(Guid entityId);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
