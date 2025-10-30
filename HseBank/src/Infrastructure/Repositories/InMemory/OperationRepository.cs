using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Repositories.InMemory
{
    public class OperationRepository : IOperationRepository
    {
        private readonly Dictionary<Guid, Operation> _operations = new();

        public void Add(Operation operation)
        {
            _operations[operation.Id] = operation;
        }

        public Operation? GetById(Guid operationId)
        {
            return _operations.ContainsKey(operationId) ? _operations[operationId] : null;
        }

        public void Update(Operation operation)
        {
            if (_operations.ContainsKey(operation.Id))
            {
                _operations[operation.Id] = operation;
            }
        }

        public void Delete(Operation operation)
        {
            _operations.Remove(operation.Id);
        }
    }
}
