using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Factories
{
    public interface IFactory<TEntity, TCreateRequest> where TEntity : EntityBase
    {
        public TEntity Create(TCreateRequest request);
    }
}
