using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Repositories.InMemory
{
    /// <summary>
    /// Репозиторий операций в памяти.
    /// </summary>
    public class OperationRepository : IOperationRepository
    {
        private readonly Dictionary<Guid, Operation> _operations = new();

        /// <summary>
        /// Добавление операции.
        /// </summary>
        /// <param name="operation">Операция.</param>
        public void Add(Operation operation)
        {
            if (_operations.ContainsKey(operation.Id))
            {
                throw new ValidationException("Операция с таким айди уже существует.");
            }
            _operations[operation.Id] = operation;
        }

        /// <summary>
        /// Получение операции по айди.
        /// </summary>
        /// <param name="operationId">Айди.</param>
        /// <returns>Операция.</returns>
        public Operation? GetById(Guid operationId)
        {
            return _operations.ContainsKey(operationId) ? _operations[operationId] : null;
        }

        /// <summary>
        /// Получение всех операций.
        /// </summary>
        /// <returns>Операции.</returns>
        public IEnumerable<Operation> GetAll()
        {
            return _operations.Values;
        }

        /// <summary>
        /// Обновление данных об операции.
        /// </summary>
        /// <param name="operation">Операция.</param>
        public void Update(Operation operation)
        {
            if (_operations.ContainsKey(operation.Id))
            {
                _operations[operation.Id] = operation;
            }
        }

        /// <summary>
        /// Удаление операции.
        /// </summary>
        /// <param name="operation">Операция.</param>
        public void Delete(Operation operation)
        {
            _operations.Remove(operation.Id);
        }
    }
}
