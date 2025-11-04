using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем операций.
    /// </summary>
    public interface IOperationRepository : IRepository<Operation>
    {
    }
}
