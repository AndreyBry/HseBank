using HseBank.src.Domain.Interfaces.Validators;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Domain.Interfaces.Services
{
    /// <summary>
    /// Предоставляет методы для валидации операций по счетам.
    /// </summary>
    public interface IOperationValidator : IValidator<OperationApplyRequest>
    {
    }
}
