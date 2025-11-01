using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        public  void Add(TEntity entity);
        public TEntity? GetById(Guid entityId);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
    }
}
