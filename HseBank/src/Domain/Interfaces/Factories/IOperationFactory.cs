using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Domain.Interfaces.Factories
{
    /// <summary>
    /// Предоставляет методы для корректного создания операций по счетам.
    /// </summary>
    public interface IOperationFactory : IFactory<Operation, OperationApplyRequest>
    {
    }
}
