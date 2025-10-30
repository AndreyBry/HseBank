using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    public interface IOperationRepository
    {
        void Add(Operation operation);
        Operation? GetById(Guid operationId);
        void Update(Operation operation);
        void Delete(Operation operation);
    }
}
